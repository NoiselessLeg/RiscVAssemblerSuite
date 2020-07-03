using Assembler.FormsGui.Utility;
using Assembler.FormsGui.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Controls
{
   public partial class HexValueGrid : UserControl
   {
      private class HexEditorByteProvider : Be.Windows.Forms.IByteProvider
      {
         public HexEditorByteProvider(CompiledFileViewModel vm)
         {
            m_ViewModel = vm;
         }

         public long Length => m_ViewModel.Data.Count;

         public event EventHandler LengthChanged;
         public event EventHandler Changed;

         public void ApplyChanges()
         {
            m_ViewModel.SaveFile();
         }

         public void DeleteBytes(long index, long length)
         {
            // not implemented.
         }

         public bool HasChanges()
         {
            return m_IsModified;
         }

         public void InsertBytes(long index, byte[] bs)
         {
         }

         public byte ReadByte(long index)
         {
            return m_ViewModel.Data[(int)index];
         }

         public bool SupportsDeleteBytes()
         {
            return false;
         }

         public bool SupportsInsertBytes()
         {
            return false;
         }

         public bool SupportsWriteByte()
         {
            return true;
         }

         public void WriteByte(long index, byte value)
         {
            m_ViewModel.Data[(int)index] = value;
            m_IsModified = true;
         }

         private readonly CompiledFileViewModel m_ViewModel;
         private bool m_IsModified;
      }

      public HexValueGrid()
      {
         m_ViewModel = new CompiledFileViewModel();
         InitializeComponent();
         m_HexEditor.ByteProvider = new HexEditorByteProvider(m_ViewModel);

      }

      public HexValueGrid(CompiledFileViewModel vm)
      {
         m_ViewModel = vm;
         InitializeComponent();
         m_HexEditor.ByteProvider = new HexEditorByteProvider(vm);
      }
      
      private readonly CompiledFileViewModel m_ViewModel;

   }
}
