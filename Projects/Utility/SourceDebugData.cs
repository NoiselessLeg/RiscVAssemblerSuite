using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   /// <summary>
   /// Contains mapping information between a set of instructions and the lines from the original
   /// assembly source file that they came from.
   /// </summary>
   public class SourceDebugData
   {
      /// <summary>
      /// Initializes the source debug data object with a provided assembly
      /// source file path.
      /// </summary>
      /// <param name="srcFilePath">The path to the assembly source file.</param>
      public SourceDebugData(string srcFilePath)
      {
         m_SrcFilePath = srcFilePath;
         m_SrcLineInfo = new List<SourceLineInformation>();
         m_SrcLineDic = new Dictionary<int, SourceLineInformation>();
      }

      /// <summary>
      /// Adds a SourceLineInformation structure to the internal list of source information.
      /// </summary>
      /// <param name="info">The instruction information.</param>
      public void AddSourceLineInformation(SourceLineInformation info)
      {
         m_SrcLineInfo.Add(info);
         m_SrcLineDic.Add(info.TextSegmentAddress, info);
      }

      /// <summary>
      /// Determines if a line from the originating assembly source file correlates
      /// to the passed in address. This is useful for psuedo-instructions, that may have
      /// multiple actual instructions synthesized.
      /// </summary>
      /// <param name="textSegmentAddr">The address in the .text segment to see if a source line exists for.</param>
      /// <returns>True if a source line correlates for the address, otherwise returns false.</returns>
      public bool IsSourceTextAssociatedWithAddress(int textSegmentAddr)
      {
         return m_SrcLineDic.ContainsKey(textSegmentAddr);
      }

      /// <summary>
      /// Gets the line number from the originating assembly source file associated with a program
      /// address. It is assumed that a caller has checked that the address is actually mapped
      /// to a source file line number by calling IsSourceTextAssociatedWithAddress prior to making
      /// this call, as this does not check address validity.
      /// </summary>
      /// <param name="address">The address to obtain the source file line number for.</param>
      /// <returns>The line number from the source file assembled into this compiled file.</returns>
      public int GetLineNumberAssociatedWithAddress(int address)
      {
         return m_SrcLineDic[address].SourceFileLineNumber;
      }

      /// <summary>
      /// Gets the absolute path of the source file that was assembled.
      /// </summary>
      public string SourceFilePath
      {
         get { return m_SrcFilePath; }
      }

      /// <summary>
      /// Gets an IEnumerable of all of the available source line information.
      /// </summary>
      public IEnumerable<SourceLineInformation> SourceInfo
      {
         get { return m_SrcLineInfo; }
      }

      private readonly string m_SrcFilePath;
      private readonly List<SourceLineInformation> m_SrcLineInfo;
      private readonly Dictionary<int, SourceLineInformation> m_SrcLineDic;
   }
}
