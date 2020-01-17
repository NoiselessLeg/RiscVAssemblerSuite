using System;
using System.Collections.Generic;

namespace Assembler.Common
{
   /// <summary>
   /// Represents a class that allows for the opposite operations as that of the SymbolTable,
   /// i.e. this class provides methods to search a label by address versus searching an address by label.
   /// Useful for disassembling.
   /// </summary>
   public class ReverseSymbolTable
   {
      /// <summary>
      /// Creates a new instance of the reverse symbol table.
      /// </summary>
      public ReverseSymbolTable()
      {
         m_Table = new Dictionary<int, Symbol>();
      }

      /// <summary>
      /// Adds a symbol to the reverse lookup table table. If the address already exists, this will throw
      /// an ArgumentException.
      /// </summary>
      /// <param name="address">The address to add as the key value.</param>
      /// <param name="labelName">The label associated with the address.</param>
      public void AddSymbol(int address, Symbol symbol)
      {
         m_Table.Add(address, symbol);
      }

      /// <summary>
      /// Returns a value determining if the label exists in the symbol table.
      /// </summary>
      /// <param name="address">The address to look up.</param>
      /// <returns>True if the address exists in the table and is mapped to a label, otherwise returns false.</returns>
      public bool ContainsSymbol(int address)
      {
         return m_Table.ContainsKey(address);
      }

      /// <summary>
      /// Fetches a symbol instance mapped to a provided address from the reverse symbol table. If the address does not exist,
      /// this will throw an ArgumentException.
      /// </summary>
      /// <param name="address">The address of the symbol to retrieve.</param>
      /// <returns>A Symbol instance mapped to the provided address.</returns>
      public Symbol GetSymbol(int address)
      {
         Symbol sym = default(Symbol);
         if (!m_Table.TryGetValue(address, out sym))
         {
            throw new ArgumentException("Could not find symbol at address " + address + " in table.");
         }

         return sym;
      }

      /// <summary>
      /// Gets an IEnumerable instance of all symbols found inthe compiled file.
      /// </summary>
      public IEnumerable<Symbol> AllSymbols => m_Table.Values;

      /// <summary>
      /// Gets the number of symbols currently in the reverse symbol table.
      /// </summary>
      public int NumSymbols => m_Table.Count;

      private readonly Dictionary<int, Symbol> m_Table;
   }
}
