using Assembler.Common;
using Assembler.Util;
using System.Linq;

namespace Assembler.SymbolTableConstruction.SymbolBuilders
{
   internal class DataSymbolBuilder : ISymbolTableBuilder
   {
      /// <summary>
      /// Creates a DataSegmentParser instance.
      /// TODO: determine if signedness/unsignedness has any tangible effect
      /// on code generation.
      /// </summary>
      public DataSymbolBuilder()
      {
         m_CurrDataAddress = CommonConstants.BASE_DATA_ADDRESS;
      }

      /// <summary>
      /// Reads a denoted .data segment line of an assembly program for symbols
      /// </summary>
      /// <param name="reader">The reader used to read the file.</param>
      /// <param name="symbolList">The list of symbols that will be added to.</param>
      /// <param name="startingLine">The line that the .data segment starts on. Will be incremented</param>
      public void ParseSymbolsInLine(LineData asmLine, SymbolTable symbolList, int alignment)
      {
         string[] tokens = asmLine.Text.Split(' ', '\t');
         string[] fixedTokens = ParserCommon.GetTrimmedTokenArray(tokens).ToArray();

         // a label should end with a ':' character and should be the first token.
         if (ParserCommon.ContainsLabel(fixedTokens[0]))
         {
            ParseLabeledLine(symbolList, asmLine, fixedTokens, alignment);
         }

         // if this doesn't have a label, and is not empty or a comment,
         // then this is a data element.
         else
         {
            // try to make sure this isn't garbage.
            // this will return false if no data was parsed, 
            if (!ParseUnlabeledLine(asmLine, fixedTokens, alignment))
            {
               throw new AssemblyException(asmLine.LineNum, "Expected size declaration, received \"" + asmLine.Text + '\"');
            }
         }
      }

      /// <summary>
      /// Parses a labeled line for symbols, and calculates the appropriate address of the element (if any).
      /// </summary>
      /// <param name="symTable">The symbol table to add the label to.</param>
      /// <param name="originalLine">The line data being parsed.</param>
      /// <param name="tokens">The string array of space-separated tokens.</param>
      /// <param name="alignment">The current alignment</param>
      private void ParseLabeledLine(SymbolTable symTable, LineData originalLine, string[] tokens, int alignment)
      {
         // if we're trying to figure out the backing data size of a label still (e.g.
         // label was declared, then the data declaration followed a few newlines later),
         // we shouldn't be seeing another label.
         if (m_UnresolvedSym != null)
         {
            throw new AssemblyException(originalLine.LineNum, "Expected data declaration after label.");
         }
         string labelName = ParserCommon.ExtractLabel(tokens[0]);
         var label = new Symbol(labelName, SegmentType.Data, m_CurrDataAddress);
         symTable.AddSymbol(label);
         m_UnresolvedSym = label;

         // if we couldn't find any following data elements, list this symbol as unresolved.
         ParseUnlabeledLine(originalLine, tokens, alignment);
      }

      /// <summary>
      /// Parses an unlabeled line to calculate the appropriate address of the next element (if any).
      /// </summary>
      /// <param name="originalLine">The line data being parsed.</param>
      /// <param name="tokens">The string array of space-separated tokens.</param>
      /// <param name="alignment">The current alignment</param>
      /// <returns>A boolean determining if anything of use was parsed. If this is false,
      /// the line should be examined to make sure a symbol was at least parsed. Otherwise,
      /// this could indicate that garbage was on the line.</returns>
      private bool ParseUnlabeledLine(LineData originalLine, string[] tokens, int alignment)
      {
         bool foundDataDeclaration = false;
         int dataDeclarationIdx = 0;

         // scan it for a data size (e.g. .asciiz, .word, etc)
         for (int i = 0; i < tokens.Length && !foundDataDeclaration; ++i)
         {
            if (ParserCommon.IsDataDeclaration(tokens[i]))
            {
               foundDataDeclaration = true;
               dataDeclarationIdx = i;
            }
         }

         // we found a data declaration; make sure that there's at least one value following it.
         if (foundDataDeclaration)
         {
            int dataSize = 0;
            if (dataDeclarationIdx + 1 < tokens.Length)
            {
               // if it is a trivial type, use our precomputed map to get the size.
               if (ParserCommon.IsTrivialDataType(tokens[dataDeclarationIdx]))
               {
                  dataSize = ParserCommon.DetermineTrivialDataSize(tokens[dataDeclarationIdx]);
                  int numElementsToStore = ParserCommon.GetArraySize(originalLine.Text, tokens[dataDeclarationIdx]);
                  int reservedSize = numElementsToStore * dataSize;
                  int paddingSize = ParserCommon.GetNumPaddingBytes(reservedSize, alignment);

                  m_CurrDataAddress += (reservedSize + paddingSize);
               }

               // otherwise, we'd expect there to be another token after the data type.
               // see if we can figure out the string length
               else if (ParserCommon.IsStringDeclaration(tokens[dataDeclarationIdx]))
               {
                  // if this is a string declaration, then get the original string data
                  string dataStr = ParserCommon.GetStringData(originalLine.Text);

                  dataSize = ParserCommon.DetermineNonTrivialDataLength(tokens[dataDeclarationIdx], dataStr);

                  int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, alignment);
                  m_CurrDataAddress += (dataSize + paddingSize);
               }

               // otherwise, this must be a .space declaration. just get the size following it.
               else
               {
                  dataSize = ParserCommon.DetermineNonTrivialDataLength(tokens[dataDeclarationIdx], tokens[dataDeclarationIdx + 1]);

                  int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, alignment);
                  m_CurrDataAddress += (dataSize + paddingSize);
               }
            }
            else
            {
               throw new AssemblyException(originalLine.LineNum, "Expected data value after token " + tokens[dataDeclarationIdx]);
            }

            if (m_UnresolvedSym != null)
            {
               m_UnresolvedSym.Size = dataSize;
               m_UnresolvedSym = null;
            }

         }

         return foundDataDeclaration;
      }

      private Symbol m_UnresolvedSym;
      private int m_CurrDataAddress;
   }
}
