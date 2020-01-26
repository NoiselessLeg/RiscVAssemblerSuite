using Assembler.Common;
using System;

namespace Assembler.CmdLine
{
   internal class ConsoleTerminal : ITerminal
   {
      public void PrintChar(char c)
      {
         Console.Write(c);
      }

      public void PrintInt(int value)
      {
         Console.Write(value);
      }

      public void PrintString(string value)
      {
         Console.Write(value);
      }

      public char ReadChar()
      {
         string value = Console.ReadLine();
         return char.Parse(value);
      }

      public int ReadInt()
      {
         string value = Console.ReadLine();
         return int.Parse(value);
      }

      public string ReadString()
      {
         return Console.ReadLine();
      }

      public void RequestOutputFlush()
      {
         Console.Out.Flush();
      }

      public void InterruptInputOperation()
      {

      }
   }
}
