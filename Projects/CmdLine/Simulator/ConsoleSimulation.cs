using Assembler.CmdLine.LoggerTypes;
using Assembler.Interpreter;
using Assembler.OutputProcessing;
using Assembler.OutputProcessing.FileReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator
{
   public class ConsoleSimulation
   {
      public ConsoleSimulation(string inputFileName, CommandInterpreter interpreter)
      {
         m_Terminal = new ConsoleEmulator(interpreter);
         m_Logger = new ConsoleLogger();
         m_TerminationMgr = new TerminationManager(inputFileName);

         var fileParserFac = new FileReaderFactory();
         ICompiledFileReader fileParser = fileParserFac.GetFileParser(inputFileName);
         DisassembledFileBase file = fileParser.ParseFile(inputFileName, m_Logger);

         m_Terminal.PrintString("Successfully loaded " + inputFileName + 
            " (source file: " + file.SourceInformation.SourceFilePath + "; " + file.TotalFileSize + " bytes)\n");

         m_ExecCtx = new RuntimeProcess(file, m_Terminal);

         IEnumerable<InstructionData> programInstructions =
            DisassemblerServices.GenerateInstructionData(file.SymbolTable, file.TextSegment, file.SourceInformation);

         m_SrcMapping = new Dictionary<int, SourceLineInformation>();

         foreach (InstructionData instructionElem in programInstructions)
         {
            m_SrcMapping.Add(instructionElem.ProgramCounterLocation, new SourceLineInformation(instructionElem));
         }

         m_CmdTable = new CommandTable(m_SrcMapping, m_ExecCtx, m_Terminal, m_TerminationMgr);

         m_Terminal.AddAvailableCommands(m_CmdTable.AllCommands);
      }

      public void RunSimulator()
      {
         m_Terminal.PrintString("Type \"help\" (sans quotes) to print a brief description of available commands.\n");
         m_Terminal.PrintString("Type \"help(<command name>)\" (sans quotes) to print more details about a specific command.\n");

         while (!m_TerminationMgr.IsTerminated)
         {
            try
            {
               m_Terminal.PrintString(">>> ");
               string command = m_Terminal.ReadCommand();

               ParsedCommand parsedCmd = m_LastParsedCmd;
               if (!string.IsNullOrEmpty(command))
               {
                  parsedCmd = ParsedCommand.ParseInput(command);
               }
               
               if (parsedCmd != null)
               {
                  m_LastParsedCmd = parsedCmd;
                  IConsoleCommand cmdImpl = m_CmdTable.GetCommand(parsedCmd);
                  if (cmdImpl == null)
                  {
                     IEnumerable<string> possibleCmds = m_Terminal.GetSuggestedCommands(command);
                     var allCmdSuggestions = new List<IConsoleCommand>();

                     foreach (var possibleCmd in possibleCmds)
                     {
                        allCmdSuggestions.AddRange(m_CmdTable.FindCommandsSimilarToTypedCommand(possibleCmd));
                     }

                     if (allCmdSuggestions.Count == 1 && allCmdSuggestions[0].NumArguments == 0)
                     {
                        cmdImpl = allCmdSuggestions[0];
                        cmdImpl.Execute(new string[] { });
                     }

                     else if (allCmdSuggestions.Count >= 1)
                     {
                        m_Terminal.PrintString("\"" + parsedCmd.Command + "\" is ambiguous. ");
                        m_Terminal.PrintString("The following command(s) match the provided pattern:\n");
                        foreach (var cmd in allCmdSuggestions)
                        {
                           m_Terminal.PrintString(cmd.CommandStringWithArgs + '\n');
                        }

                        m_Terminal.PrintChar('\n');
                     }
                     else
                     {
                        m_Terminal.PrintString("\"" + parsedCmd.Command + "\" was not a recognized command. ");
                        m_Terminal.PrintChar('\n');
                     }
                  }
                  else
                  {
                     cmdImpl.Execute(parsedCmd.Arguments);
                  }
               }
            }
            catch (Exception ex)
            {
               m_Terminal.PrintString(ex.Message + '\n');
               m_Terminal.RequestOutputFlush();
            }
         }
      }

      public bool IsSimulatorRestarting
      {
         get { return m_TerminationMgr.IsSimulatorRestarting; }
      }

      public string AsmFileToLoadOnRestart
      {
         get { return m_TerminationMgr.AsmFileToLoad; }
      }

      private ParsedCommand m_LastParsedCmd;
      private readonly ConsoleEmulator m_Terminal;
      private readonly ConsoleLogger m_Logger;
      private readonly RuntimeProcess m_ExecCtx;
      private readonly CommandTable m_CmdTable;

      private readonly TerminationManager m_TerminationMgr;

      private readonly Dictionary<int, SourceLineInformation> m_SrcMapping;
   }
}
