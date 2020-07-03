using Assembler.FormsGui.ViewModels;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assembler.FormsGui.Services;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Assembler.Common;

namespace Assembler.FormsGui.Controls
{
   public partial class AssemblyTextBox : UserControl, IDisposable
   {
      public AssemblyTextBox()
      {
         InitializeComponent();
      }

      public AssemblyTextBox(AssemblyFileViewModel avm,
                             PreferencesViewModel preferences) :
         this()
      {
         m_ViewModel = avm;
         m_ViewModel.FileErrors.CollectionChanged += OnFileErrorsChanged;

         preferencesViewModelBindingSource.DataSource = preferences;
         assemblyFileViewModelBindingSource.DataSource = avm;
         m_FileTxtBox.SetHighlighting("Assembly");
      }

      public string ActiveFileName
      {
         get
         {
            return m_ViewModel.FileName;
         }
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
            if (m_ViewModel != null)
            {
               m_ViewModel.FileErrors.CollectionChanged -= OnFileErrorsChanged;
            }
         }
         base.Dispose(disposing);
      }

      private void OnFileErrorsChanged(object sender, NotifyCollectionChangedEventArgs e)
      {
         // get the line of each syntax error.

         switch (e.Action)
         {
            case NotifyCollectionChangedAction.Add:
            {
               foreach (var item in e.NewItems)
               {
                  var error = item as AssemblyException;
                  int zeroBasedLineNum = error.LineNumber - 1;
                  LineSegment offendingLine = m_FileTxtBox.Document.LineSegmentCollection[zeroBasedLineNum];
                  int lineOffset = offendingLine.Offset;
                  int lineLen = offendingLine.Length;
                  int firstNonWhitespaceCol = offendingLine.Words.GetFirstNonWhitespaceColumn();
                  var errorMarker = new TextMarker(lineOffset + firstNonWhitespaceCol, lineLen - firstNonWhitespaceCol, 
                     TextMarkerType.WaveLine, Color.Red);
                  m_FileTxtBox.Document.MarkerStrategy.AddMarker(errorMarker);
                  m_FileTxtBox.Update();
               }

               m_FileTxtBox.Refresh();
               break;
            }

            case NotifyCollectionChangedAction.Remove:
            {
               foreach (var item in e.OldItems)
               {
                  var error = item as AssemblyException;
                  LineSegment offendingLine = m_FileTxtBox.Document.LineSegmentCollection[error.LineNumber];
                  m_FileTxtBox.Document.MarkerStrategy.RemoveAll((marker) => marker.Offset == offendingLine.Offset);
               }

               break;
            }
         }
      }
      
      private readonly AssemblyFileViewModel m_ViewModel;
   }
}
