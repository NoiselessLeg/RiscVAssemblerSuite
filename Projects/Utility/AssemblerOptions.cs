
using CommandLine;
using System.Collections.Generic;

namespace Assembler.Common
{
   [Verb("assemble", HelpText = "Assembles a series of files.")]
   public class AssemblerOptions
   {

      /// <summary>
      /// Creates an instance of the options that the assembler will use.
      /// </summary>
      /// <param name="inputFileNames">The list of input file names to be assembled.</param>
      /// <param name="logFile">The name of the log file, if any, to output to.</param>
      /// <param name="runAfterAssembly">If true, the interpreter will execute the assembled file after code is generated.</param>
      public AssemblerOptions(IEnumerable<string> inputFileNames, 
                              IEnumerable<string> outputFileNames,
                              string logFile = "",
                              bool runAfterAssembly = false)
      {
         m_InputFileNames = inputFileNames;
         m_OutputFileNames = outputFileNames;
         m_LogFile = logFile;
         m_RunAfterAssembly = runAfterAssembly;
      }

      /// <summary>
      /// Gets the list of files to assemble.
      /// </summary>
      [Option('i', "input", Required = true, HelpText = "Input assembly files to be assembled.")]
      public IEnumerable<string> InputFileNames => m_InputFileNames;

      /// <summary>
      /// Gets the list of files to assemble.
      /// </summary>
      [Option('o', "output", Required = true, HelpText = "Output files to be generated. These are respective to the files provided in the input file list.")]
      public IEnumerable<string> OutputFileNames => m_OutputFileNames;


      /// <summary>
      /// The log file that any log data will be written out to.
      /// </summary>
      [Option('l', "logfile", Required = false, HelpText = "Log file to output to (optional).")]
      public string LogFile => m_LogFile;

      /// <summary>
      /// Gets a boolean value representing if the file will be executed by the runtime after assembly.
      /// </summary>
      [Option('r', "run-after", Default = false, Required = false, HelpText = "Run this file after successful assembly.")]
      public bool RunAfterAssembly => m_RunAfterAssembly;

      private readonly IEnumerable<string> m_InputFileNames;
      private readonly IEnumerable<string> m_OutputFileNames;
      private readonly string m_LogFile;
      private readonly bool m_RunAfterAssembly;
   }
}
