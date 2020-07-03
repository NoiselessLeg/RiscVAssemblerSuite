using Assembler.Common;
using Assembler.OutputProcessing.TextOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing
{
   /// <summary>
   /// Represents a completely disassembled file.
   /// </summary>
   public abstract class DisassembledFileBase
   {
      /// <summary>
      /// Gets the total file size of the disassembled file.
      /// </summary>
      public int TotalFileSize
      {
         get { return DataSegmentLength + TextSegment.SegmentSize + ExternSegmentSize; }
      }

      /// <summary>
      /// Gets the accessor to this file's data segment.
      /// </summary>
      public DataSegmentAccessor DataSegment
      {
         get { return m_DataSegment; }
      }
      
      /// <summary>
      /// Gets the used size of the program's .data segment.
      /// </summary>
      public int DataSegmentLength
      {
         get { return m_DataSegment.SegmentSize; }
      }
      
      /// <summary>
      /// Gets the accessor to this file's text segment.
      /// </summary>
      public TextSegmentAccessor TextSegment
      {
         get { return m_TextSegment; }
      }

      /// <summary>
      /// Gets the size of this file's extern segment, in bytes.
      /// </summary>
      public int ExternSegmentSize
      {
         get { return m_ExternSize; }
      }

      /// <summary>
      /// Gets the reverse-lookup symbol table parsed from this file.
      /// </summary>
      public ReverseSymbolTable SymbolTable
      {
         get { return m_SymTbl; }
      }

      /// <summary>
      /// Gets the assembly source file information for this file.
      /// </summary>
      public SourceDebugData SourceInformation
      {
         get { return m_DbgInfo; }
      }

      /// <summary>
      /// Gets the implementation of the file writer that can generate a 
      /// text file (i.e. disassembled and human-readable assembly file).
      /// </summary>
      public abstract IAssemblyFileWriter AssemblyTextFileWriter { get; }

      /// <summary>
      /// Creates an instance of the disassembled file.
      /// </summary>
      /// <param name="dataSegment">The disassembled .data segment of the file.</param>
      /// <param name="instructions">The disassembled .text segment of the file.</param>
      /// <param name="externSegment">The constituent bytes of the .extern segment.</param>
      /// <param name="symTable">The symbol table associated with the file.</param>
      /// <param name="dbgData">The source file information associated with the file.</param>
      protected DisassembledFileBase(DataSegmentAccessor dataSegment, TextSegmentAccessor instructions,
                                     IEnumerable<byte> externSegment, ReverseSymbolTable symTable,
                                     SourceDebugData dbgData)
      {
         m_DataSegment = dataSegment;
         m_TextSegment = instructions;
         m_ExternSize = externSegment.Count();
         m_SymTbl = symTable;
         m_DbgInfo = dbgData;
      }

      private readonly DataSegmentAccessor m_DataSegment;
      private readonly TextSegmentAccessor m_TextSegment;
      private readonly int m_ExternSize;
      private readonly ReverseSymbolTable m_SymTbl;
      private readonly SourceDebugData m_DbgInfo;
   }
}
