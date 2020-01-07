using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.DataModels
{
   public class DataSegmentChangedEventArgs : EventArgs
   {
      public DataSegmentChangedEventArgs(int startingAddress, int numWordsChanged)
      {
         m_StartingAddress = startingAddress;
         m_NumWordsChanged = numWordsChanged;
      }

      public int StartingAddress
      {
         get { return m_StartingAddress; }
      }

      public int NumWordsUpdated
      {
         get { return m_NumWordsChanged; }
      }

      private readonly int m_StartingAddress;
      private readonly int m_NumWordsChanged;
   }

   public class BindableDataSegmentAccessor : Interpreter.IDataSegmentAccessor
   {
      public event EventHandler<DataSegmentChangedEventArgs> DataSegmentWritten;

      public BindableDataSegmentAccessor(OutputProcessing.DataSegmentAccessor accessor)
      {
         m_UnderlyingAccessor = new Interpreter.RuntimeDataSegmentAccessor(accessor);
      }

      public int Sbrk(int amountToAllocate)
      {
         return m_UnderlyingAccessor.Sbrk(amountToAllocate);
      }

      public sbyte ReadSignedByte(int address)
      {
         return m_UnderlyingAccessor.ReadSignedByte(address);
      }

      public byte ReadUnsignedByte(int address)
      {
         return m_UnderlyingAccessor.ReadUnsignedByte(address);
      }

      public short ReadShort(int address)
      {
         return m_UnderlyingAccessor.ReadShort(address);
      }

      public ushort ReadUnsignedShort(int address)
      {
         return m_UnderlyingAccessor.ReadUnsignedShort(address);
      }

      public int ReadWord(int address)
      {
         return m_UnderlyingAccessor.ReadWord(address);
      }

      public uint ReadUnsignedWord(int address)
      {
         return m_UnderlyingAccessor.ReadUnsignedWord(address);
      }

      public long ReadLong(int address)
      {
         return m_UnderlyingAccessor.ReadLong(address);
      }

      public ulong ReadUnsignedLong(int address)
      {
         return m_UnderlyingAccessor.ReadUnsignedLong(address);
      }

      public string ReadString(int address)
      {
         return m_UnderlyingAccessor.ReadString(address);
      }

      public void WriteSignedByte(int address, sbyte value)
      {
         m_UnderlyingAccessor.WriteSignedByte(address, value);
         OnSegmentChanged(address, 1);
      }

      public void WriteUnsignedByte(int address, byte value)
      {
         m_UnderlyingAccessor.WriteUnsignedByte(address, value);
         OnSegmentChanged(address, 1);
      }

      public void WriteShort(int address, short value)
      {
         m_UnderlyingAccessor.WriteShort(address, value);
         OnSegmentChanged(address, 1);
      }

      public void WriteUnsignedShort(int address, ushort value)
      {
         m_UnderlyingAccessor.WriteUnsignedShort(address, value);
         OnSegmentChanged(address, 1);
      }

      public void WriteWord(int address, int value)
      {
         m_UnderlyingAccessor.WriteWord(address, value);
         OnSegmentChanged(address, 1);
      }

      public void WriteUnsignedWord(int address, uint value)
      {
         m_UnderlyingAccessor.WriteUnsignedWord(address, value);
         OnSegmentChanged(address, 1);
      }

      public void WriteLong(int address, long value)
      {
         m_UnderlyingAccessor.WriteLong(address, value);
         OnSegmentChanged(address, 2);
      }

      public void WriteUnsignedLong(int address, ulong value)
      {
         m_UnderlyingAccessor.WriteUnsignedLong(address, value);
         OnSegmentChanged(address, 2);
      }

      public void WriteString(int address, string str)
      {
         m_UnderlyingAccessor.WriteString(address, str);

         // string is not a word size, so increment the number
         // of words that changed by one (since a byte may
         // have changed in a nearby word).
         int strWordSize = str.Length / sizeof(int);
         if (str.Length % sizeof(int) != 0)
         {
            ++strWordSize;
         }
         OnSegmentChanged(address, strWordSize);
      }

      protected virtual void OnSegmentChanged(int address, int numWords)
      {
         for (int i = 0; i < numWords; ++i)
         {
            DataSegmentWritten?.Invoke(this, new DataSegmentChangedEventArgs(address, numWords));
         }
      }

      private readonly Interpreter.RuntimeDataSegmentAccessor m_UnderlyingAccessor;
   }
}
