using Assembler.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   class DirCommand : IConsoleCommand
   {
      public DirCommand(ITerminal terminal)
      {
         m_Terminal = terminal;
      }

      public string CommandString => "ls";

      public string CommandStringWithArgs => CommandString + "()";

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + "-> prints the entries in the current directory.";
         }
      }

      public int NumArguments => 0;

      public void Execute(string[] args)
      {
         var directory = Directory.GetCurrentDirectory();
         foreach (var entry in Directory.EnumerateFileSystemEntries(directory))
         {
            string fileName = entry;
            if (fileName.Contains('\\'))
            {
               int fileNameStartIdx = fileName.LastIndexOf('\\') + 1;
               fileName = fileName.Substring(fileNameStartIdx, fileName.Length - fileNameStartIdx);

            }
            m_Terminal.PrintString(fileName + '\n');
         }
      }

      private readonly ITerminal m_Terminal;
   }
}
