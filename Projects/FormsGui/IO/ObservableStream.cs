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

   /// <summary>
   /// Stream that provides a notification to users who wish to listen
   /// for and retrieve written data.
   /// </summary>
   public class ObservableStream : Stream
   {
      /// <summary>
      /// Occurs when data is written to the stream.
      /// </summary>
      public event EventHandler<DataWrittenEventArgs> OnDataWritten;

      /// <summary>
      /// Creates an instance of an ObservableStream.
      /// </summary>
      public ObservableStream()
      {
         m_Strm = new MemoryStream();
         m_Underlying = new BufferedStream(m_Strm);
      }

      /// <summary>
      /// Gets a value indicating whether the current stream supports reading.
      /// </summary>
      public override bool CanRead => m_Underlying.CanRead;

      /// <summary>
      /// Gets a value indicating whether the current stream supports seeking.
      /// </summary>
      public override bool CanSeek => m_Underlying.CanSeek;

      /// <summary>
      /// Gets a value indicating whether the current stream supports writing.
      /// </summary>
      public override bool CanWrite => m_Underlying.CanWrite;

      /// <summary>
      /// Gets the stream length in bytes.
      /// </summary>
      public override long Length => m_Underlying.Length;

      public override long Position { get => m_Underlying.Position; set => m_Underlying.Position = value; }

      public override void Flush()
      {
         m_Underlying.Flush();
      }

      /// <summary>
      /// Copies bytes from the current stream to an array.
      /// </summary>
      /// <param name="buffer">The buffer to which bytes are to be copied.</param>
      /// <param name="offset">The byte offset in the buffer at which to begin reading bytes.</param>
      /// <param name="count">The number of bytes to be read.</param>
      /// <returns>The total number of bytes read into array. This can be less than the number of
      //     bytes requested if that many bytes are not currently available, or 0 if the end
      //     of the stream has been reached before any data can be read.</returns>
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
