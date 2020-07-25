using Assembler.CmdLine.Simulator.ConsoleCommands;
using Assembler.Common;
using Assembler.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator
{
   public class CommandTable
   {
      public CommandTable(Dictionary<int, SourceLineInformation> srcData, 
                          RuntimeProcess executive, 
                          ITerminal terminal, 
                          TerminationManager termMgr)
      {
         var tmpCmdList = new List<IConsoleCommand>()
         {
            new PwdCommand(terminal),
            new DirCommand(terminal),
            new ChangeDirectoryCommand(),
            new DumpRegistersCommand(executive.RegisterManager, terminal),
            new DumpMemoryCommand(executive, terminal),
            new FormatAndDumpRegistersCommand(executive.RegisterManager, terminal),
            new FormatAndDumpMemoryCommand(executive, terminal),
            new ReadRegisterCommand(executive.RegisterManager, terminal),
            new FormatAndReadRegisterCommand(executive.RegisterManager, terminal),
            new ReadMemoryCommand(executive, terminal),
            new FormatAndReadMemoryCommand(executive, terminal),
            new WriteRegisterCommand(executive.RegisterManager, terminal),
            new WriteMemoryCommand(executive, terminal),
            new RunProcessCommand(executive, terminal),
            new InstructionStepCommand(executive, terminal),
            new SetBreakpointCommand(srcData, executive, terminal),
            new ContinueExecutionCommand(executive, terminal),
            new QuitCommand(executive, terminal, termMgr),
            new LoadFileCommand(executive, terminal, termMgr)
         };

         m_CmdData = new Dictionary<CommandKey, IConsoleCommand>();
         foreach (var cmd in tmpCmdList)
         {
            m_CmdData.Add(new CommandKey(cmd.CommandString, cmd.NumArguments), cmd);
         }

         m_OverallHelpCmd = new GenericHelpCommand(this, terminal);

         m_CmdData.Add(new CommandKey(m_OverallHelpCmd.CommandString, m_OverallHelpCmd.NumArguments), m_OverallHelpCmd);
      }

      public IEnumerable<IConsoleCommand> GetAllOverloadsForCommand(string command)
      {
         var retList = new List<IConsoleCommand>();
         foreach (var cmd in m_CmdData)
         {
            if (cmd.Key.Command == command)
            {
               retList.Add(cmd.Value);
            }
         }

         return retList;
      }

      public IEnumerable<IConsoleCommand> AllCommands
      {
         get
         {
            return m_CmdData.Values;
         }
      }

      public IConsoleCommand GetCommand(ParsedCommand userCmd)
      {
         var lookupData = new CommandKey(userCmd.Command, userCmd.NumArguments);

         if (!m_CmdData.TryGetValue(lookupData, out IConsoleCommand cmd))
         {
            // see if "cmd" was a help command (these are separate since they rely on the command
            // table being built prior to their construction).
            if (userCmd.Command == "help" && userCmd.NumArguments == 0)
            {
               cmd = m_OverallHelpCmd;
            }
         }

         return cmd;
      }

      public IEnumerable<IConsoleCommand> FindCommandsSimilarToTypedCommand(string cmdStr)
      {
         var cmdList = new List<IConsoleCommand>();
         foreach (var cmd in m_CmdData)
         {
            if (cmd.Key.Command == cmdStr)
            {
               cmdList.Add(cmd.Value);
            }
         }

         return cmdList;
      }

      class CommandKey
      {
         public CommandKey(string command, int argCount)
         {
            m_Cmd = command;
            m_ArgCount = argCount;
         }

         public override bool Equals(object obj)
         {
            var other = obj as CommandKey;
            if (other == null)
            {
               return false;
            }

            return other.Command.Equals(Command) && other.ArgumentCount.Equals(ArgumentCount);
         }

         public override int GetHashCode()
         {
            return StringComparer.InvariantCulture.GetHashCode(m_Cmd) + m_ArgCount.GetHashCode();
         }

         public string Command => m_Cmd;
         public int ArgumentCount => m_ArgCount;

         private readonly string m_Cmd;
         private readonly int m_ArgCount;

      }

      private static IEnumerable<IConsoleCommand> GetAllImplementedConsoleCommands()
      {
         var type = typeof(IConsoleCommand);
         var typeList = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract && p.IsClass);

         var retList = new List<IConsoleCommand>();
         foreach (var domainType in typeList)
         {
            retList.Add((IConsoleCommand)Activator.CreateInstance(domainType));
         }

         return retList;
      }
      
      private readonly Dictionary<CommandKey, IConsoleCommand> m_CmdData;
      private readonly GenericHelpCommand m_OverallHelpCmd;
      
   }
}
