using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator.ConsoleCommands
{
   public class ChangeDirectoryCommand : IConsoleCommand
   {
      public string CommandString => "cd";

      public string CommandStringWithArgs => CommandString + "(<new directory>)";

      public string HelpText
      {
         get
         {
            return CommandStringWithArgs + "-> changes the active directory to the new directory.";
         }
      }

      public int NumArguments => 1;

      public void Execute(string[] args)
      {
         Directory.SetCurrentDirectory(args[0]);
      }
   }
}
