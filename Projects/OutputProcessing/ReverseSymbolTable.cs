using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing
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
            m_Table = new Dictionary<int, string>();
        }

        /// <summary>
        /// Adds a symbol to the reverse lookup table table. If the address already exists, this will throw
        /// an ArgumentException.
        /// </summary>
        /// <param name="address">The address to add as the key value.</param>
        /// <param name="labelName">The label associated with the address.</param>
        public void AddSymbol(int address, string labelName)
        {
            m_Table.Add(address, labelName);
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
        /// Fetches a label mapped to a provided address from the reverse symbol table. If the address does not exist,
        /// this will throw an ArgumentException.
        /// </summary>
        /// <param name="address">The symbol name to retrieve.</param>
        /// <returns>A Label instance containing the address and found segment type of the symbol.</returns>
        public string GetLabel(int address)
        {
            string lbl = string.Empty;
            if (!m_Table.TryGetValue(address, out lbl))
            {
                throw new ArgumentException("Could not find symbol address " + address + " in symbol table.");
            }

            return lbl;
        }

        /// <summary>
        /// Gets the number of symbols currently in the reverse symbol table.
        /// </summary>
        public int NumSymbols
        {
            get { return m_Table.Count; }
        }

        private readonly Dictionary<int, string> m_Table;
    }
}
