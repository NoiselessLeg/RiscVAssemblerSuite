using Assembler.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class AssemblerExecutionConsole : ITerminal, IDisposable
   {
      public AssemblerExecutionConsole(Stream inputStream, Stream outputStream)
      {
         m_InputReader = new StreamReader(inputStream);
         m_OutputWriter = new StreamWriter(outputStream);
      }

      public void PrintChar(char c)
      {
         m_OutputWriter.Write(c);
      }

      public void PrintInt(int value)
      {
         m_OutputWriter.Write(value);
      }

      public void PrintString(string value)
      {
         m_OutputWriter.Write(value);
      }

      public char ReadChar()
      {
         m_OutputWriter.Flush();
         return (char) m_InputReader.Read();
      }

      public int ReadInt()
      {
         m_OutputWriter.Flush();
         string line = null;
         do
         {
            line = m_InputReader.ReadLine();
         }
         while (line == null);
         return Convert.ToInt32(line);
      }

      public string ReadString()
      {
         m_OutputWriter.Flush();
         string line = null;
         do
         {
            line = m_InputReader.ReadLine();
         }
         while (line == null);
         return line;
      }

      public void InterruptInputOperation()
      {
         var inputStream = m_InputReader.BaseStream as IO.InputStream;
         inputStream.AbortRead();
      }

      public void RequestOutputFlush()
      {
         m_OutputWriter.Flush();
      }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      protected virtual void Dispose(bool disposing)
      {
         if (disposing)
         {
            m_OutputWriter.Dispose();
            m_InputReader.Dispose();
         }
      }

      private readonly StreamWriter m_OutputWriter;
      private readonly StreamReader m_InputReader;
   }
}
