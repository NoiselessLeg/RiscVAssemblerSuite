using Assembler.FormsGui.Utility;
using Assembler.FormsGui.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Controls
{
   public partial class FileExecutionTabPage : UserControl
   {
      public FileExecutionTabPage()
      {
         InitializeComponent();
      }

      public FileExecutionTabPage(DisassembledFileViewModel viewModel):
         this()
      {
         m_PrimarySrcGridRowColor = m_SrcGrid.DefaultCellStyle.BackColor;
         m_AlternateSrcGridRowColor = m_SrcGrid.AlternatingRowsDefaultCellStyle.BackColor;

         m_ShowDecValuesItem.Click += OnShowDecimalValuesToolbarItemClick;
         m_ShowHexValuesItem.Click += OnShowHexValuesToolbarItemClick;
         m_ShowDataElemsAsDecimalBtn.Click += OnShowDataElemsAsDecimalToolbarItemClick;
         m_ShowDataElemsAsHexBtn.Click += OnShowDataElemsAsHexToolbarItemClick;

         m_FileViewModel = viewModel;
         m_ExConsole = new AssemblerExecutionConsole(m_ConsoleTxt.InputStream, m_ConsoleTxt.OutputStream);

         m_ExViewModel = new ExecutionViewModel(m_ExConsole, viewModel, viewModel.InstructionList);

         var usrRegBindingAdapter = new BindingList<RegisterViewModel>(m_ExViewModel.Registers);
         m_UsrRegisterData.AutoGenerateColumns = false;
         m_UsrRegisterData.DataSource = usrRegBindingAdapter;

         var fpRegBindingAdapter = new BindingList<FloatingPointRegisterViewModel>(m_ExViewModel.FloatingPointRegisters);
         m_FpRegisterData.AutoGenerateColumns = false;
         m_FpRegisterData.DataSource = fpRegBindingAdapter;

         m_DataSegmentGrdView.AutoGenerateColumns = false;
         m_DataSegmentGrdView.DataSource = m_ExViewModel.DataElements;
         usrRegisterBindingSource.DataSource = usrRegBindingAdapter;
         fpRegBindingSource.DataSource = fpRegBindingAdapter;


         jefFileViewModelBindingSource.DataSource = m_FileViewModel;
         m_ExViewModel.PropertyChanged += OnExecutionViewModelChanged;

         // just bind the "isEnabled" property here since we have an event handler
         // here to clear the console, then execute the underlying view model command.
         // we can't convert this to a simple binding statement like the other buttons have
         // because of this.
         BindingHelper.BindPredicateToEnabledProperty(m_StartBtn, m_ExViewModel.ExecuteFileUntilEndCommand);

         m_TerminateBtn.BindToCommand(m_ExViewModel.TerminateExecutionCommand);
         m_PauseBtn.BindToCommand(m_ExViewModel.PauseExecutionCommand);
         m_ResumeBtn.BindToCommand(m_ExViewModel.ResumeExecutionCommand);
         m_StepBtn.BindToCommand(m_ExViewModel.StepToNextInstructionCommand);

         // set the first row by default to be highlighted.
         RemoveRowHighlighting(0);
         UpdateRowHighlighting(0);
      }

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }

         if (disposing)
         {
            m_ShowDataElemsAsHexBtn.Click -= OnShowDataElemsAsHexToolbarItemClick;
            m_ShowDataElemsAsDecimalBtn.Click -= OnShowDataElemsAsDecimalToolbarItemClick;
            m_ShowHexValuesItem.Click -= OnShowHexValuesToolbarItemClick;
            m_ShowDecValuesItem.Click -= OnShowDecimalValuesToolbarItemClick;
         }

         base.Dispose(disposing);
      }

      private void OnStartButtonClicked(object sender, EventArgs e)
      {
         m_ConsoleTxt.ClearConsole();
         m_ExViewModel.ExecuteFileUntilEndCommand.Execute(null);
      }

      private void OnExecutionViewModelChanged(object sender, PropertyChangedEventArgs e)
      {
         var viewModel = sender as ExecutionViewModel;
         if (e.PropertyName == nameof(viewModel.ActiveInstructionIdx))
         {
            // bound these, in case the last instruction is the end of the .text segment.
            int prevRowIdx = Math.Min(viewModel.PreviousInstructionIndex, m_SrcGrid.RowCount - 1);
            int rowIdx = Math.Min(viewModel.ActiveInstructionIdx, m_SrcGrid.RowCount - 1);

            // if we are not the first row, invalidate the previous row so we lose
            // the highlighting that was there (since it has changed).
            RemoveRowHighlighting(prevRowIdx);
            UpdateRowHighlighting(rowIdx);
         }
      }

      // used because the datagrid view doesn't do any commits until the cell is changed.
      // this will force our event to trigger once we actually click the breakpoint.
      private void OnBreakpointSet(object sender, DataGridViewCellMouseEventArgs e)
      {
         const int BREAKPOINT_COL_IDX = 0;
         const int INVALID_ROW_IDX = -1;
         if (e.ColumnIndex == BREAKPOINT_COL_IDX && e.RowIndex != INVALID_ROW_IDX)
         {
            m_SrcGrid.EndEdit();
         }
      }

      private void RemoveRowHighlighting(int rowIndex)
      {
         // bound the row index.
         if (rowIndex % 2 == 0)
         {
            m_SrcGrid.Rows[rowIndex].DefaultCellStyle.SelectionBackColor = m_PrimarySrcGridRowColor;
            m_SrcGrid.Rows[rowIndex].DefaultCellStyle.BackColor = m_PrimarySrcGridRowColor;
         }
         else
         {
            m_SrcGrid.Rows[rowIndex].DefaultCellStyle.SelectionBackColor = m_AlternateSrcGridRowColor;
            m_SrcGrid.Rows[rowIndex].DefaultCellStyle.BackColor = m_AlternateSrcGridRowColor;
         }
      }

      private void UpdateRowHighlighting(int rowIndex)
      {
         m_SrcGrid.Rows[rowIndex].DefaultCellStyle.SelectionBackColor = Color.Yellow;
         m_SrcGrid.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Yellow;

         // invoking this in our run loop is HYPER expensive. This is literally just
         // to move the selected index so it's visible to the user if it goes offscreen,
         // so only do that if we're confident the selection moved off screen.
         if (CurrentElemIndexIsOffscreen(rowIndex))
         {
            m_SrcGrid.InvokeIfRequired(() => m_SrcGrid.FirstDisplayedScrollingRowIndex = rowIndex);
         }
      }
      
      private bool CurrentElemIndexIsOffscreen(int rowIndex)
      {
         // calculate the highlighted row index to see if it scrolled outside of the display.
         int dgvSizeInCells = m_SrcGrid.Height / m_SrcGrid.CurrentCell.Size.Height;

         int firstDisplayedCell = m_SrcGrid.FirstDisplayedScrollingRowIndex;

         // in case the current row index goes back up above our first displayed cell.
         int indexFromFirstDisplayedCell = rowIndex - firstDisplayedCell;
         bool elemOffscreen = false;
         if (indexFromFirstDisplayedCell >= dgvSizeInCells ||
             indexFromFirstDisplayedCell < 0)
         {
            elemOffscreen = true;
         }
         
         return elemOffscreen;
      }

      private void OnShowDataElemsAsHexToolbarItemClick(object sender, EventArgs args)
      {

         var btn = sender as ToolStripMenuItem;
         if (btn.Checked)
         {
            m_ExViewModel.ChangeDataValueDisplayTypeCommand.Execute(RegisterDisplayType.Hexadecimal);
            m_ShowDataElemsAsDecimalBtn.Checked = false;
         }
         else
         {
            btn.Checked = true;
         }
      }

      private void OnShowDataElemsAsDecimalToolbarItemClick(object sender, EventArgs e)
      {
         var btn = sender as ToolStripMenuItem;
         if (btn.Checked)
         {
            m_ExViewModel.ChangeDataValueDisplayTypeCommand.Execute(RegisterDisplayType.Decimal);
            m_ShowDataElemsAsHexBtn.Checked = false;
         }
         else
         {
            btn.Checked = true;
         }
      }

      private void OnShowHexValuesToolbarItemClick(object sender, EventArgs e)
      {
         var btn = sender as ToolStripMenuItem;
         if (btn.Checked)
         {
            m_ExViewModel.ChangeRegisterValueDisplayTypeCommand.Execute(RegisterDisplayType.Hexadecimal);
            m_ShowDecValuesItem.Checked = false;
         }
         else
         {
            btn.Checked = true;
         }
      }

      private void OnShowDecimalValuesToolbarItemClick(object sender, EventArgs e)
      {
         var btn = sender as ToolStripMenuItem;
         if (btn.Checked)
         {
            m_ExViewModel.ChangeRegisterValueDisplayTypeCommand.Execute(RegisterDisplayType.Decimal);
            m_ShowHexValuesItem.Checked = false;
         }
         else
         {
            btn.Checked = true;
         }
      }

      // used for restoring rows to their former luster when the selected index
      // moves.
      private readonly Color m_PrimarySrcGridRowColor;
      private readonly Color m_AlternateSrcGridRowColor;

      private readonly ExecutionViewModel m_ExViewModel;
      private readonly DisassembledFileViewModel m_FileViewModel;
      private readonly AssemblerExecutionConsole m_ExConsole;
   }
}
