﻿using Assembler.Common;
using Assembler.Output.ObjFileComponents;
using System.Collections.Generic;

namespace Assembler.Output
{
   /// <summary>
   /// Represents the data that will be stored in any form of output file.
   /// </summary>
   internal class BasicObjectFile
   {
      /// <summary>
      /// Creates an instance of the basic object file.
      /// </summary>
      /// <param name="symTable">The symbol table that will be stored away.</param>
      public BasicObjectFile(SymbolTable symTable)
      {
         m_SymTable = symTable;
         m_DataElements = new List<IObjectFileComponent>();
         m_TextElements = new List<IObjectFileComponent>();
         m_ExternElements = new List<IObjectFileComponent>();
      }

      /// <summary>
      /// Adds a number of byte elements to the .extern section of this .obj file.
      /// </summary>
      /// <param name="sizeInBytes">The number of bytes to reserve for this object.</param>
      public virtual void AddExternElement(int sizeInBytes)
      {
         for (int i = 0; i < sizeInBytes; ++i)
         {
            m_ExternElements.Add(new ByteDataElement(0));
         }
      }

      /// <summary>
      /// Adds an 8-bit data element to this .obj file.
      /// </summary>
      /// <param name="dataElement">The 8 bit value to add.</param>
      public virtual void AddDataElement(byte dataElement)
      {
         m_DataElements.Add(new ByteDataElement(dataElement));
      }

      /// <summary>
      /// Adds a 16-bit data element to this .obj file.
      /// </summary>
      /// <param name="dataElement">The 16 bit value to add.</param>
      public virtual void AddDataElement(short dataElement)
      {
         m_DataElements.Add(new Int16DataElement(dataElement));
      }

      /// <summary>
      /// Adds a 32-bit data element to this .obj file.
      /// </summary>
      /// <param name="dataElement">The 32 bit value to add.</param>
      public virtual void AddDataElement(int dataElement)
      {
         m_DataElements.Add(new Int32DataElement(dataElement));
      }

      /// <summary>
      /// Adds a 64-bit data element to this .obj file.
      /// </summary>
      /// <param name="dataElement">The 64 bit value to add.</param>
      public virtual void AddDataElement(long dataElement)
      {
         m_DataElements.Add(new Int64DataElement(dataElement));
      }

      /// <summary>
      /// Adds a non-null-terminated ASCII data element to this .obj file.
      /// </summary>
      /// <param name="str">The string to add.</param>
      public virtual void AddAsciiString(string str)
      {
         m_DataElements.Add(new AsciiDataSegmentElement(str));
      }

      /// <summary>
      /// Adds a null-terminated ASCII data element to this .obj file.
      /// </summary>
      /// <param name="str">The string to add. This will implicitly be null terminated.</param>
      public virtual void AddNullTerminatedAsciiString(string str)
      {
         m_DataElements.Add(new AsciizDataSegmentElement(str));
      }

      /// <summary>
      /// Adds a new alignment change declaration to the metadata segment. This
      /// does not add any new data to the data segment.
      /// </summary>
      /// <param name="newAlignment">The new alignment to use.</param>
      public virtual void AddAlignmentChangeDeclaration(int newAlignment)
      {
         m_DataElements.Add(new AlignmentChangeDataSegmentElement(newAlignment));
      }

      /// <summary>
      /// Adds a .text element to this .obj file.
      /// </summary>
      /// <param name="instruction">The 32-bit instruction to add.</param>
      public virtual void AddInstruction(int instruction)
      {
         m_TextElements.Add(new Int32DataElement(instruction));
      }

      /// <summary>
      /// Gets an IEnumerable of all saved .text elements in this .obj file.
      /// </summary>
      public IEnumerable<IObjectFileComponent> TextElements => m_TextElements;

      /// <summary>
      /// Gets an IEnumerable of all saved .data elements in this .obj file.
      /// </summary>
      public IEnumerable<IObjectFileComponent> DataElements => m_DataElements;

      /// <summary>
      /// Gets an IEnumerable of all .extern elements in this .obj file.
      /// </summary>
      public IEnumerable<IObjectFileComponent> ExternElements => m_ExternElements;

      /// <summary>
      /// Gets the symbol table used by this object file.
      /// </summary>
      public SymbolTable SymbolTable => m_SymTable;

      private readonly SymbolTable m_SymTable;
      private readonly List<IObjectFileComponent> m_TextElements;
      private readonly List<IObjectFileComponent> m_DataElements;
      private readonly List<IObjectFileComponent> m_ExternElements;
   }
}
