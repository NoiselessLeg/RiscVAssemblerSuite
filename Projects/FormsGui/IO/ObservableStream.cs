using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.IO
{
   public class DataWrittenEventArgs : EventArgs
   {
      public DataWrittenEventArgs(int writeOffset, int numBytesWritten)
      {
         m_Offset = writeOffset;
         m_NumBytesWritten = numBytesWritten;
      }

      public int Offset { get { return m_Offset; } }
      public int NumBytesWritten { get { return m_NumBytesWritten; } }

      private readonly int m_Offset;
      private readonly int m_NumBytesWritten;
   }

   public class ObservableStream : Stream
   {
      public event EventHandler<DataWrittenEventArgs> OnDataWritten;

      public ObservableStream()
      {
         m_Strm = new MemoryStream();
         m_Underlying = new BufferedStream(m_Strm);
      }

      public override bool CanRead => m_Underlying.CanRead;

      public override bool CanSeek => m_Underlying.CanSeek;

      public override bool CanWrite => m_Underlying.CanWrite;

      public override long Length => m_Underlying.Length;

      public override long Position { get => m_Underlying.Position; set => m_Underlying.Position = value; }

      public override void Flush()
      {
         m_Underlying.Flush();
      }

      public override int Read(byte[] buffer, int offset, int count)
      {
         // seek back enough so we can see a read-value.
         m_Underlying.Seek(m_Underlying.Position - count, SeekOrigin.Begin);
         return m_Underlying.Read(buffer, offset, count);
      }

      public override long Seek(long offset, SeekOrigin origin)
      {
         return m_Underlying.Seek(offset, origin);
      }

      public override void SetLength(long value)
      {
         m_Underlying.SetLength(value);
      }

      public override void Write(byte[] buffer, int offset, int count)
      {
         m_Underlying.Write(buffer, offset, count);
         var eventArgs = new DataWrittenEventArgs(offset, count);
         OnDataInsertion(eventArgs);
      }

      protected virtual void OnDataInsertion(DataWrittenEventArgs e)
      {
         OnDataWritten?.Invoke(this, e);
      }

      private readonly MemoryStream m_Strm;
      private readonly BufferedStream m_Underlying;
   }
}
