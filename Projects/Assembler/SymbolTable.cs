using System;
using System.Collections.Generic;

namespace Assembler
{
    /// <summary>
    /// Represents a mapping of symbol names to their addresses in program memory.
    /// </summary>
    class SymbolTable
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
            m_Table.Add(label.LabelName, label);
        }

        /// <summary>
        /// Fetches a symbol with a given name from the symbol table. If the symbol does not exist,
        /// this will throw an ArgumentException.
        /// </summary>
        /// <param name="symbolName">The symbol name to retrieve.</param>
        /// <returns>A Label instance containing the address and found segment type of the symbol.</returns>
        public Symbol GetSymbol(string symbolName)
        {
            Symbol lbl = default(Symbol);
            if (!m_Table.TryGetValue(symbolName, out lbl))
            {
                throw new ArgumentException("Could not find symbol name " + symbolName + " in symbol table.");
            }

            return lbl;
        }

        /// <summary>
        /// Gets the number of symbols currently in the symbol table.
        /// </summary>
        public int NumSymbols
        {
            get { return m_Table.Count; }
        }
        private readonly Dictionary<string, Symbol> m_Table;
    }
}
