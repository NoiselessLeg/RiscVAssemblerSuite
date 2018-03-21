using Assembler.Common;
using Assembler.Output.ObjFileComponents;
using System.Collections.Generic;

namespace Assembler.Output
{
    /// <summary>
    /// Represents the data that will be stored in any form of output file.
    /// </summary>
    class BasicObjectFile
    {
        /// <summary>
        /// Creates an instance of the basic object file.
        /// </summary>
        /// <param name="symTable">The symbol table that will be stored away.</param>
        public BasicObjectFile(SymbolTable symTable, Endianness outputEndianness)
        {
            m_SymTable = symTable;
            m_Endianness = outputEndianness;
            m_DataElements = new List<IObjectFileComponent>();
            m_TextElements = new List<IObjectFileComponent>();
        }

        /// <summary>
        /// Adds an 8-bit data element to this .obj file.
        /// </summary>
        /// <param name="dataElement">The 8 bit value to add.</param>
        public void AddDataElement(byte dataElement)
        {
            m_DataElements.Add(new ByteDataElement(dataElement));
        }

        /// <summary>
        /// Adds a 16-bit data element to this .obj file.
        /// </summary>
        /// <param name="dataElement">The 16 bit value to add.</param>
        public void AddDataElement(short dataElement)
        {
            m_DataElements.Add(new Int16DataElement(dataElement, m_Endianness));
        }

        /// <summary>
        /// Adds a 32-bit data element to this .obj file.
        /// </summary>
        /// <param name="dataElement">The 32 bit value to add.</param>
        public void AddDataElement(int dataElement)
        {
            m_DataElements.Add(new Int32DataElement(dataElement, m_Endianness));
        }

        /// <summary>
        /// Adds a 64-bit data element to this .obj file.
        /// </summary>
        /// <param name="dataElement">The 64 bit value to add.</param>
        public void AddDataElement(long dataElement)
        {
            m_DataElements.Add(new Int64DataElement(dataElement, m_Endianness));
        }

        /// <summary>
        /// Adds a non-null-terminated ASCII data element to this .obj file.
        /// </summary>
        /// <param name="str">The string to add.</param>
        public void AddAsciiString(string str)
        {
            m_DataElements.Add(new AsciiDataSegmentElement(str));
        }

        /// <summary>
        /// Adds a null-terminated ASCII data element to this .obj file.
        /// </summary>
        /// <param name="str">The string to add. This will implicitly be null terminated.</param>
        public void AddNullTerminatedAsciiString(string str)
        {
            m_DataElements.Add(new AsciizDataSegmentElement(str));
        }

        /// <summary>
        /// Adds a .text element to this .obj file.
        /// </summary>
        /// <param name="instruction">The 32-bit instruction to add.</param>
        public void AddInstruction(int instruction)
        {
            m_TextElements.Add(new Int32DataElement(instruction, m_Endianness));
        }

        /// <summary>
        /// Gets an IEnumerable of all saved .text elements in this .obj file.
        /// </summary>
        public IEnumerable<IObjectFileComponent> TextElements
        {
            get { return m_TextElements; }
        }

        /// <summary>
        /// Gets an IEnumerable of all saved .data elements in this .obj file.
        /// </summary>
        public IEnumerable<IObjectFileComponent> DataElements
        {
            get { return m_DataElements; }
        }

        /// <summary>
        /// Gets the symbol table used by this object file.
        /// </summary>
        public SymbolTable SymbolTable
        {
            get { return m_SymTable; }
        }

        private readonly SymbolTable m_SymTable;
        private readonly Endianness m_Endianness;
        private readonly List<IObjectFileComponent> m_TextElements;
        private readonly List<IObjectFileComponent> m_DataElements;
    }
}
