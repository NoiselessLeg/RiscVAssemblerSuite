using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assembler.FormsGui.IO
{
   public class InputStream : MemoryStream
   {
      public InputStream()
      {
         m_DataReady = new ManualResetEvent(false);
         m_Buffers = new ConcurrentQueue<byte[]>();
      }

      public override int Read(byte[] buffer, int offset, int count)
      {
         m_DataReady.WaitOne();
         int ret = 0;

         if (!m_Buffers.TryDequeue(out byte[] lBuffer))
         {
            m_DataReady.Reset();
         }
         else
         {
            if (!IsDataAvailable)
            {
               m_DataReady.Reset();
            }

            int numBytesRead = Math.Min(lBuffer.Length, count);
            Array.Copy(lBuffer, 0, buffer, offset, numBytesRead);
            ret = numBytesRead;
         }

         return ret;
      }

      public override void Write(byte[] buffer, int offset, int count)
      {
         m_Buffers.Enqueue(buffer);
         m_DataReady.Set();
      }

      public bool IsDataAvailable { get { return m_Buffers.IsEmpty; } }

      private readonly ManualResetEvent m_DataReady;
      private readonly ConcurrentQueue<byte[]> m_Buffers;
   }
}
