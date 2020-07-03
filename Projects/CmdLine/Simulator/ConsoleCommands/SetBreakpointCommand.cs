using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   class SetBreakpointCommand : IConsoleCommand
   {
      public SetBreakpointCommand(Dictionary<int, SourceLineInformation> srcLineData, RuntimeProcess exec, ITerminal terminal)
      {
         m_SrcData = srcLineData;
         m_Exec = exec;
         m_Terminal = terminal;
      }

      public string CommandString => "setBkPt";

      public string CommandStringWithArgs => CommandString + "(<.text address>)\n" + CommandString + "(<file name : line number>)";

      public int NumArguments => 1;

      public string HelpText
      {
         get
         {
            var helpBuilder = new StringBuilder();
            helpBuilder.AppendLine(CommandString + "() -> this command is overloaded:");
            helpBuilder.AppendLine("\t\t" + CommandString + "(<.text address>) -> places a breakpoint at the specified .text address that will be triggered " +
                                   "once the program counter reaches the address.");
            helpBuilder.AppendLine("\t\t" + CommandString + "(<file name : line number>) -> places a break point at the .text address mapped to the provided " +
                                   "source ine number that will be triggered once the program counter reaches the address.");


            return helpBuilder.ToString();
         }
      }

      public void Execute(string[] args)
      {
         if (!IntExtensions.TryParseEx(args[0], out int pgmCounter))
         {
            // if the argument was not a program counter arg, try to find the source line.
            string srcData = args[0];

            string[] splitData = srcData.Split(':');
            if (splitData.Length == 2)
            {
               for (int i = 0; i < 2; ++i)
               {
                  splitData[i] = splitData[i].Trim();
               }

               int srcLineNum = int.Parse(splitData[1]);

               bool found = false;
               foreach (var srcLine in m_SrcData.Values)
               {
                  if (srcLine.SourceFilePath == splitData[0] && srcLine.SourceLineNumber == srcLineNum)
                  {
                     pgmCounter = srcLine.ProgramCounterLocation;
                     found = true;
                     break;
                  }
               }

               if (!found)
               {
                  m_Terminal.PrintString("Could not attach breakpoint to line " + srcLineNum + " of file " + splitData[0] + '\n');
               }
            }
         }

         if (m_SrcData.ContainsKey(pgmCounter))
         {
            m_Exec.SetBreakpoint(pgmCounter);
            m_Terminal.PrintString("Successfully attached breakpoint to source address 0x" + pgmCounter.ToString("x8") + '\n');
         }
         else
         {
            m_Terminal.PrintString("Could not attach breakpoint to given source address.\n");
         }
      }

      private readonly Dictionary<int, SourceLineInformation> m_SrcData;
      private readonly RuntimeProcess m_Exec;
      private readonly ITerminal m_Terminal;
   }
}

