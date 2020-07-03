using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.TextOutput
{
   /// <summary>
   /// Defines a class that is able to generate a textual
   /// assembly source file from an implementation-defined
   /// disassembled file format.
   /// </summary>
   public interface IAssemblyFileWriter
   {
      /// <summary>
      /// Generates a textual assembly source file from
      /// a disassembled file format.
      /// </summary>
      /// <param name="outputFileName">The file path to output the file to.</param>
      void GenerateOutputFile(string outputFileName);
   }
}
