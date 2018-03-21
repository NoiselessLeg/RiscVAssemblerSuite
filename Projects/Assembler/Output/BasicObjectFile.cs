using Assembler.Output.ObjFileComponents;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output
{
    class BasicObjectFile
    {
        public BasicObjectFile(SymbolTable symTable)
        {
            m_SymTable = symTable;
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
            m_DataElements.Add(new Int16DataElement(dataElement));
        }

        /// <summary>
        /// Adds a 32-bit data element to this .obj file.
        /// </summary>
        /// <param name="dataElement">The 32 bit value to add.</param>
        public void AddDataElement(int dataElement)
        {
            m_DataElements.Add(new Int32DataElement(dataElement));
        }

        /// <summary>
        /// Adds a 64-bit data element to this .obj file.
        /// </summary>
        /// <param name="dataElement">The 64 bit value to add.</param>
        public void AddDataElement(long dataElement)
        {
            m_DataElements.Add(new Int64DataElement(dataElement));
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
            m_TextElements.Add(new Int32DataElement(instruction));
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
        private readonly List<IObjectFileComponent> m_TextElements;
        private readonly List<IObjectFileComponent> m_DataElements;
    }
}
