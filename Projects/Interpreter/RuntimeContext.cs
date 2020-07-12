using Assembler.Simulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assembler.Interpreter
{
   /// <summary>
   /// Provides an abstraction to system calls to retrieve data from or to modify the
   /// runtime environment.
   /// </summary>
   public class RuntimeContext
   {
      public RuntimeContext(IRuntimeEnvironment env, IDataSegmentAccessor dataSegment, RegisterManager regMgr)
      {

         m_Environment = env;
         m_DataAccessor = dataSegment;
         m_RegMgr = regMgr;
      }

      /// <summary>
      /// Simulates an sbrk() syscall. This will allocate the next available word size on the heap memory.
      /// If the requested amount is not word aligned, this will over-allocate to the next word boundary.
      /// </summary>
      /// <param name="amountToAllocate">The number of bytes to allocate.</param>
      /// <returns>The address of the new block, or -1 if the system is out of memory.</returns>
      public int Sbrk(int amountToAllocate)
      {
         return m_DataAccessor.Sbrk(amountToAllocate);
      }

      /// <summary>
      /// Reads a byte from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
      /// <returns>The byte stored at the provided address.</returns>
      public sbyte ReadSignedByte(int address)
      {
         return m_DataAccessor.ReadSignedByte(address);
      }

      /// <summary>
      /// Reads an unsigned byte from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
      /// <returns>The byte stored at the provided address.</returns>
      public byte ReadUnsignedByte(int address)
      {
         return m_DataAccessor.ReadUnsignedByte(address);
      }

      /// <summary>
      /// Reads a 16 bit signed integer from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 16-bit signed integer stored at the provided address.</returns>
      public short ReadShort(int address)
      {
         return m_DataAccessor.ReadShort(address);
      }

      /// <summary>
      /// Reads a 16 bit unsigned integer from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 16-bit unsigned integer stored at the provided address.</returns>
      public ushort ReadUnsignedShort(int address)
      {
         return m_DataAccessor.ReadUnsignedShort(address);
      }

      /// <summary>
      /// Reads a 32 bit signed integer from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 32-bit signed integer stored at the provided address.</returns>
      public int ReadWord(int address)
      {
         return m_DataAccessor.ReadWord(address);
      }

      /// <summary>
      /// Reads a 32 bit unsigned integer from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 32-bit unsigned integer stored at the provided address.</returns>
      public uint ReadUnsignedWord(int address)
      {
         return m_DataAccessor.ReadUnsignedWord(address);
      }

      /// <summary>
      /// Reads a 64 bit signed integer from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 64-bit signed integer stored at the provided address.</returns>
      public long ReadLong(int address)
      {
         return m_DataAccessor.ReadLong(address);
      }

      /// <summary>
      /// Reads a 64 bit unsigned integer from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 64-bit unsigned integer stored at the provided address.</returns>
      public ulong ReadUnsignedLong(int address)
      {
         return m_DataAccessor.ReadUnsignedLong(address);
      }

      /// <summary>
      /// Reads a null-terminated ASCII string from the .data segment.
      /// </summary>
      /// <param name="address">The address to retrieve the string from.</param>
      /// <returns>A string encoded in the ASCII encoding.</returns>
      public string ReadString(int address)
      {
         return m_DataAccessor.ReadString(address);
      }

      /// <summary>
      /// Writes a signed byte into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      public void WriteSignedByte(int address, sbyte value)
      {
         m_DataAccessor.WriteSignedByte(address, value);
      }

      /// <summary>
      /// Writes an unsigned byte into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      public void WriteUnsignedByte(int address, byte value)
      {
         m_DataAccessor.WriteUnsignedByte(address, value);
      }

      /// <summary>
      /// Writes a 16-bit signed integer into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      public void WriteShort(int address, short value)
      {
         m_DataAccessor.WriteShort(address, value);
      }

      /// <summary>
      /// Writes a 16-bit unsigned integer into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      public void WriteUnsignedShort(int address, ushort value)
      {
         m_DataAccessor.WriteUnsignedShort(address, value);
      }

      /// <summary>
      /// Writes a 32-bit signed integer into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      public void WriteWord(int address, int value)
      {
         m_DataAccessor.WriteWord(address, value);
      }

      /// <summary>
      /// Writes a 32-bit unsigned integer into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      public void WriteUnsignedWord(int address, uint value)
      {
         m_DataAccessor.WriteUnsignedWord(address, value);
      }

      /// <summary>
      /// Writes a 64-bit signed integer into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      public void WriteLong(int address, long value)
      {
         m_DataAccessor.WriteLong(address, value);
      }

      /// <summary>
      /// Writes a 64-bit unsigned integer into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      public void WriteUnsignedLong(int address, ulong value)
      {
         m_DataAccessor.WriteUnsignedLong(address, value);
      }

      /// <summary>
      /// Reads a null-terminated ASCII string from the .data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      public void WriteString(int address, string str)
      {
         m_DataAccessor.WriteString(address, str);
      }

      /// <summary>
      /// Requests that the environment implementation terminates the application runtime.
      /// </summary>
      public void TerminateApplication()
      {
         m_Environment.Terminate();
      }

      /// <summary>
      /// Gets the array of registers that are read/writeable during runtime.
      /// </summary>
      public IList<IRegister<int>> UserRegisters => m_RegMgr.UserIntRegisters;

      private readonly IRuntimeEnvironment m_Environment;
      private readonly IDataSegmentAccessor m_DataAccessor;
      private readonly RegisterManager m_RegMgr;
   }
}
