using Assembler.Common;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Disassembler
{
    public class RiscVDisassembler
    {
        /// <summary>
        /// Disassembles a .JEF file into instructions and data.
        /// </summary>
        /// <param name="options">The options used to disassemble the file</param>
        /// <param name="logger">A logging implementation to log errors to.</param>
        public void Disassemble(DisassemblerOptions options, ILogger logger)
        {
            var stopwatch = new Stopwatch();

            var tasks = new List<Task<bool>>();
            stopwatch.Start();
            DisassembleFile(logger, options);
            stopwatch.Stop();

            if (tasks.Any(t => !t.Result))
            {
                logger.Log(LogLevel.Info, "Disassembly completed (with errors) in " + stopwatch.Elapsed.ToString());
            }
            else
            {
                logger.Log(LogLevel.Info, "Disassembly completed in " + stopwatch.Elapsed.ToString());
            }

        }

        /// <summary>
        /// Task for disassembling one individual file.
        /// </summary>
        /// <param name="logger">The logging implementation to log errors/info to.</param>
        /// <param name="options">The options to use while disassembling.</param>
        /// <returns>True if the disassembler could successfully disassemble the file; otherwise returns false.</returns>
        public bool DisassembleFile(ILogger logger, DisassemblerOptions options)
        {
            bool success = true;
            logger.Log(LogLevel.Info, "Invoking disassembler for file " + options.InputFileName);
            try
            {
                var jefFileProc = new JefFileProcessor();
                DisassembledFile fileBase = jefFileProc.ProcessJefFile(options.InputFileName, logger);
                var txtGen = new TextFileGenerator();
                txtGen.GenerateOutput(options.OutputFileName, fileBase);
            }
            catch (IOException ex)
            {
                logger.Log(LogLevel.Critical, ex.Message);
                success = false;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Critical, "In file " + options.InputFileName + ":");
                logger.Log(LogLevel.Critical, ex.Message);
                success = false;
            }

            return success;
        }
    }
}
