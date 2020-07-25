namespace Assembler.Interpreter
{
   public interface IDataSegmentAccessor
   {
      /// <summary>
      /// Simulates an sbrk() syscall. This will allocate the next available word size on the heap memory.
      /// If the requested amount is not word aligned, this will over-allocate to the next word boundary.
      /// </summary>
      /// <param name="amountToAllocate">The number of bytes to allocate.</param>
      /// <returns>The address of the new block, or -1 if the system is out of memory.</returns>
      int Sbrk(int amountToAllocate);

      /// <summary>
      /// Reads a byte from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
      /// <returns>The byte stored at the provided address.</returns>
      sbyte ReadSignedByte(int address);

      /// <summary>
      /// Reads an unsigned byte from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
      /// <returns>The byte stored at the provided address.</returns>
      byte ReadUnsignedByte(int address);

      /// <summary>
      /// Reads a 16 bit signed integer from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 16-bit signed integer stored at the provided address.</returns>
      short ReadShort(int address);

      /// <summary>
      /// Reads a 16 bit unsigned integer from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 16-bit unsigned integer stored at the provided address.</returns>
      ushort ReadUnsignedShort(int address);

      /// <summary>
      /// Reads a 32 bit signed integer from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 32-bit signed integer stored at the provided address.</returns>
      int ReadWord(int address);

      /// <summary>
      /// Reads a 32 bit unsigned integer from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 32-bit unsigned integer stored at the provided address.</returns>
      uint ReadUnsignedWord(int address);
      
      /// <summary>
      /// Reads a 32 bit single precision floating point value from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 32-bit float value stored at the provided address.</returns>
      float ReadSinglePrecisionFloat(int address);

      /// <summary>
      /// Reads a 64 bit signed integer from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 64-bit signed integer stored at the provided address.</returns>
      long ReadLong(int address);

      /// <summary>
      /// Reads a 64 bit unsigned integer from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 64-bit unsigned integer stored at the provided address.</returns>
      ulong ReadUnsignedLong(int address);

      /// <summary>
      /// Reads a null-terminated ASCII string from the .data segment.
      /// </summary>
      /// <param name="address">The address to retrieve the string from.</param>
      /// <returns>A string encoded in the ASCII encoding.</returns>
      string ReadString(int address);

      /// <summary>
      /// Writes a signed byte into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      void WriteSignedByte(int address, sbyte value);

      /// <summary>
      /// Writes an unsigned byte into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      void WriteUnsignedByte(int address, byte value);

      /// <summary>
      /// Writes a 16-bit signed integer into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      void WriteShort(int address, short value);

      /// <summary>
      /// Writes a 16-bit unsigned integer into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      void WriteUnsignedShort(int address, ushort value);

      /// <summary>
      /// Writes a 32-bit signed integer into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      void WriteWord(int address, int value);

      /// <summary>
      /// Writes a 32-bit unsigned integer into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      void WriteUnsignedWord(int address, uint value);

      /// <summary>
      /// Writes a 32-bit single precision flaot into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      void WriteSinglePrecisionFloat(int address, float value);

      /// <summary>
      /// Writes a 64-bit signed integer into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      void WriteLong(int address, long value);

      /// <summary>
      /// Writes a 64-bit unsigned integer into the specified .data segment offset.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      void WriteUnsignedLong(int address, ulong value);

      /// <summary>
      /// Reads a null-terminated ASCII string from the .data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to write to.</param>
      /// <param name="value">The value to write to the address.</param>
      void WriteString(int address, string str);
   }
}
