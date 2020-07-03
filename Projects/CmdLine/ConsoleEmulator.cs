using Assembler.CmdLine.Simulator;
using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine
{
   public class ConsoleEmulator : ITerminal
   {
      public ConsoleEmulator(CommandInterpreter interpreter)
      {
         m_CmdInterpreter = interpreter;
      }

      public void AddAvailableCommands(IEnumerable<IConsoleCommand> commands)
      {
         m_CmdInterpreter.AddAvailableCommands(commands);
      }

      public IEnumerable<string> GetSuggestedCommands(string typedCmd)
      {
         return m_CmdInterpreter.GetSuggestedCommands(typedCmd);
      }

      public string ReadCommand()
      {
         return m_CmdInterpreter.ReadCommand();
      }

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
         string value = null;
         while (value == null)
         {
            value = m_CmdInterpreter.ReadLine();
         }
         return char.Parse(value);
      }

      public int ReadInt()
      {
         string value = null;
         while (value == null)
         {
            value = m_CmdInterpreter.ReadLine();
         }
         
          return int.Parse(value);
      }

      public string ReadString()
      {
         string value = null;
         while (value == null)
         {
            value = m_CmdInterpreter.ReadLine();
         }
         return value;
      }

      public void RequestOutputFlush()
      {
         Console.Out.Flush();
      }

      public void InterruptInputOperation()
      {

      }

      private CommandInterpreter m_CmdInterpreter;
   }
}
