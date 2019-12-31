using System;
using System.Collections.Generic;

namespace Assembler.Common
{
   /// <summary>
   /// Represents a mapping of symbol names to their addresses in program memory.
   /// </summary>
   public class SymbolTable
   {
      /// <summary>
      /// Creates a new instance of the symbol table.
      /// </summary>
      public SymbolTable()
      {
         m_Table = new Dictionary<string, Symbol>();
      }

      /// <summary>
      /// Adds a symbol to the symbol table. If the symbol already exists, this will throw
      /// an ArgumentException.
      /// </summary>
      /// <param name="label">The label (symbol) to add.</param>
      public void AddSymbol(Symbol label)
      {
         try
         {
            m_Table.Add(label.LabelName, label);
         }
         catch (ArgumentException ex)
         {
            throw new ArgumentException("Redefinition of symbol: \"" + label.LabelName);
         }
      }

      /// <summary>
      /// Returns a value determining if the label exists in the symbol table.
      /// </summary>
      /// <param name="label">The label to look up.</param>
      /// <returns>True if the label exists as a symbol in the symbol table; otherwise returns false.</returns>
      public bool ContainsSymbol(string label)
      {
         return m_Table.ContainsKey(label);
      }

      /// <summary>
      /// Fetches a symbol with a given name from the symbol table. If the symbol does not exist,
      /// this will throw an ArgumentException.
      /// </summary>
      /// <param name="symbolName">The symbol name to retrieve.</param>
      /// <returns>A Label instance containing the address and found segment type of the symbol.</returns>
      public Symbol GetSymbol(string symbolName)
      {
         if (!m_Table.TryGetValue(symbolName, out Symbol lbl))
         {
            throw new ArgumentException("Could not find symbol name " + symbolName + " in symbol table.");
         }

         return lbl;
      }

      /// <summary>
      /// Gets the number of symbols currently in the symbol table.
      /// </summary>
      public int NumSymbols => m_Table.Count;

      /// <summary>
      /// Returns an IEnumerable of all stored symbols in this table.
      /// </summary>
      public IEnumerable<Symbol> Symbols => m_Table.Values;

      private readonly Dictionary<string, Symbol> m_Table;
   }
}
