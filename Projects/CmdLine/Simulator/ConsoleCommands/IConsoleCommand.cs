using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator
{
   public interface IConsoleCommand
   {
      string CommandString { get; }
      string CommandStringWithArgs { get; }
      string HelpText { get; }
      int NumArguments { get; }
      void Execute(string[] args);
   }
}
