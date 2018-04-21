using Assembler.Common;
using Assembler.OutputProcessing;
using Assembler.OutputProcessing.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter
{
    class RuntimeDataSegmentAccessor
    {
        public RuntimeDataSegmentAccessor(DataSegmentAccessor segmentAccessor)
        {
            m_DataSubsections = new List<DataSubsection>();

            byte[] dataBytes = new byte[CommonConstants.MAX_SEGMENT_SIZE];
            Array.Copy(segmentAccessor.RawData.ToArray(), dataBytes, segmentAccessor.RawData.Count());
            int startingDataOffset = segmentAccessor.BaseRuntimeDataAddress;
            int limitDataOffset = startingDataOffset + CommonConstants.MAX_SEGMENT_SIZE;
            DataSubsection dataSubsection = new DataSubsection(startingDataOffset, limitDataOffset, dataBytes);
            m_DataSubsections.Add(dataSubsection);

            byte[] stackBytes = new byte[CommonConstants.MAX_SEGMENT_SIZE];
            int stackBaseOffset = InterpreterCommon.STACK_BASE_OFFSET;
            int stackLimitOffset = stackBaseOffset - CommonConstants.MAX_SEGMENT_SIZE;

            // since the stack grows downward, need to provide these parameters in reverse order
            DataSubsection stackSubsection = new DataSubsection(stackLimitOffset, stackBaseOffset, stackBytes);
            m_DataSubsections.Add(stackSubsection);
            

            byte[] heapBytes = new byte[0];
            int heapBaseOffset = InterpreterCommon.HEAP_BASE_OFFSET;
            int heapLimitOffset = heapBaseOffset + CommonConstants.MAX_SEGMENT_SIZE;
            DataSubsection heapSubsection = new DataSubsection(heapBaseOffset, heapLimitOffset, heapBytes);
            m_DataSubsections.Add(heapSubsection);

            m_CurrHeapAddress = heapBaseOffset;
        }

        /// <summary>
        /// Simulates an sbrk() syscall. This will allocate the next available word size on the heap memory.
        /// If the requested amount is not word aligned, this will over-allocate to the next word boundary.
        /// </summary>
        /// <param name="amountToAllocate">The number of bytes to allocate.</param>
        /// <returns>The address of the new block, or -1 if the system is out of memory.</returns>
        public int Sbrk(int amountToAllocate)
        {
            int newAddress = m_CurrHeapAddress + amountToAllocate;
            try
            {
                // get the next word-aligned address.
                if (newAddress % sizeof(int) != 0)
                {
                    newAddress = newAddress + (sizeof(int) - (newAddress % sizeof(int)));
                }

                int wordAmtToAllocate = amountToAllocate + (sizeof(int) - (amountToAllocate % sizeof(int)));
                DataSubsection subsection = GetDataSegmentForAddress(m_CurrHeapAddress);

                int currHeapSizeBytes = subsection.RawData.Length;
                int newHeapSize = currHeapSizeBytes + wordAmtToAllocate;

                // determine if the address is out of our 4MB limit.
                // if so, return -1.
                if (newAddress >= subsection.StartingAddress + CommonConstants.MAX_SEGMENT_SIZE)
                {
                    newAddress = -1;
                }
                else
                {
                    Array.Resize(ref subsection.RawData, newHeapSize);
                }
                
            }
            catch (OutOfMemoryException)
            {
                // if our whole system is legitimately out of memory, return -1. (should never happen).
                // hopefully should be safe, since this is the only area we really dynamically
                // allocate memory during runtime.
                newAddress = -1;
            }

            return newAddress;
        }
        
        /// <summary>
        /// Reads a byte from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
        /// <returns>The byte stored at the provided address.</returns>
        public sbyte ReadSignedByte(int address)
        {
            DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
            return DataSegmentCommon.ReadSignedByte(correspondingSubsection.RawData, address, correspondingSubsection.StartingAddress);
        }

        /// <summary>
        /// Reads an unsigned byte from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
        /// <returns>The byte stored at the provided address.</returns>
        public byte ReadUnsignedByte(int address)
        {
            DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
            return DataSegmentCommon.ReadUnsignedByte(correspondingSubsection.RawData, address, correspondingSubsection.StartingAddress);
        }

        /// <summary>
        /// Reads a 16 bit signed integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 16-bit signed integer stored at the provided address.</returns>
        public short ReadShort(int address)
        {
            DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
            return DataSegmentCommon.ReadShort(correspondingSubsection.RawData, address, correspondingSubsection.StartingAddress);
        }

        /// <summary>
        /// Reads a 16 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 16-bit unsigned integer stored at the provided address.</returns>
        public ushort ReadUnsignedShort(int address)
        {
            DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
            return DataSegmentCommon.ReadUnsignedShort(correspondingSubsection.RawData, address, correspondingSubsection.StartingAddress);
        }

        /// <summary>
        /// Reads a 32 bit signed integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 32-bit signed integer stored at the provided address.</returns>
        public int ReadWord(int address)
        {
            DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
            return DataSegmentCommon.ReadWord(correspondingSubsection.RawData, address, correspondingSubsection.StartingAddress);
        }

        /// <summary>
        /// Reads a 32 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 32-bit unsigned integer stored at the provided address.</returns>
        public uint ReadUnsignedWord(int address)
        {
            DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
            return DataSegmentCommon.ReadUnsignedWord(correspondingSubsection.RawData, address, correspondingSubsection.StartingAddress);
        }

        /// <summary>
        /// Reads a 64 bit signed integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 64-bit signed integer stored at the provided address.</returns>
        public long ReadLong(int address)
        {
            DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
            return DataSegmentCommon.ReadLong(correspondingSubsection.RawData, address, correspondingSubsection.StartingAddress);
        }

        /// <summary>
        /// Reads a 64 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 64-bit unsigned integer stored at the provided address.</returns>
        public ulong ReadUnsignedLong(int address)
        {
            DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
            return DataSegmentCommon.ReadUnsignedLong(correspondingSubsection.RawData, address, correspondingSubsection.StartingAddress);
        }

        /// <summary>
        /// Reads a null-terminated ASCII string from the .data segment.
        /// </summary>
        /// <param name="address">The address to retrieve the string from.</param>
        /// <returns>A string encoded in the ASCII encoding.</returns>
        public string ReadString(int address)
        {
            DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
            return DataSegmentCommon.ReadString(correspondingSubsection.RawData, address, correspondingSubsection.StartingAddress);
        }

        /// <summary>
        /// Writes a signed byte into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteSignedByte(int address, sbyte value)
        {
            try
            {
                DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
                int idx = address - correspondingSubsection.StartingAddress;
                correspondingSubsection.RawData[idx] = (byte)value;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 8 bit memory write to address 0x" + address.ToString("X8"));
            }
        }

        /// <summary>
        /// Writes an unsigned byte into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteUnsignedByte(int address, byte value)
        {
            try
            {
                DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
                int idx = address - correspondingSubsection.StartingAddress;
                correspondingSubsection.RawData[idx] = value;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 8 bit memory write to address 0x" + address.ToString("X8"));
            }
        }

        /// <summary>
        /// Writes a 16-bit signed integer into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteShort(int address, short value)
        {
            try
            {
                DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
                int idx = address - correspondingSubsection.StartingAddress;
                byte[] shortBytes = BitConverter.GetBytes(value);
                CopyBytes(shortBytes, correspondingSubsection.RawData, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 16 bit memory write to address 0x" + address.ToString("X8"));
            }
        }

        /// <summary>
        /// Writes a 16-bit unsigned integer into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteUnsignedShort(int address, ushort value)
        {
            try
            {
                DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
                int idx = address - correspondingSubsection.StartingAddress;
                byte[] shortBytes = BitConverter.GetBytes(value);
                CopyBytes(shortBytes, correspondingSubsection.RawData, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 16 bit memory write to address 0x" + address.ToString("X8"));
            }
        }

        /// <summary>
        /// Writes a 32-bit signed integer into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteWord(int address, int value)
        {
            try
            {
                DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
                int idx = address - correspondingSubsection.StartingAddress;
                byte[] wordBytes = BitConverter.GetBytes(value);
                CopyBytes(wordBytes, correspondingSubsection.RawData, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 32 bit memory write to address 0x" + address.ToString("X8"));
            }
        }

        /// <summary>
        /// Writes a 32-bit unsigned integer into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteUnsignedWord(int address, uint value)
        {
            try
            {
                DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
                int idx = address - correspondingSubsection.StartingAddress;
                byte[] wordBytes = BitConverter.GetBytes(value);
                CopyBytes(wordBytes, correspondingSubsection.RawData, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 32 bit memory write to address 0x" + address.ToString("X8"));
            }
        }

        /// <summary>
        /// Writes a 64-bit signed integer into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteLong(int address, long value)
        {
            try
            {
                DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
                int idx = address - correspondingSubsection.StartingAddress;
                byte[] longBytes = BitConverter.GetBytes(value);
                CopyBytes(longBytes, correspondingSubsection.RawData, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 64 bit memory write to address 0x" + address.ToString("X8"));
            }
        }

        /// <summary>
        /// Writes a 64-bit unsigned integer into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteUnsignedLong(int address, ulong value)
        {
            try
            {
                DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
                int idx = address - correspondingSubsection.StartingAddress;
                byte[] longBytes = BitConverter.GetBytes(value);
                CopyBytes(longBytes, correspondingSubsection.RawData, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 64 bit memory write to address 0x" + address.ToString("X8"));
            }
        }

        /// <summary>
        /// Reads a null-terminated ASCII string from the .data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteString(int address, string str)
        {
            try
            {
                DataSubsection correspondingSubsection = GetDataSegmentForAddress(address);
                int idx = address - correspondingSubsection.StartingAddress;
                byte[] strBytes = Encoding.ASCII.GetBytes(str);
                CopyBytes(strBytes, correspondingSubsection.RawData, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds memory write to address 0x" + address.ToString("X8"));
            }
        }

        /// <summary>
        /// Copies a byte array into the main .data segment.
        /// </summary>
        /// <param name="fromArr">The bytes to be copied into the data array.</param>
        /// <param name="destArr">The array the bytes shall be copied to.</param>
        /// <param name="offset">Where in the destination array the bytes shall be copied to.</param>
        private void CopyBytes(byte[] fromArr, byte[] destArr, int offsetInDest)
        {
            for (int i = offsetInDest; i < offsetInDest + fromArr.Length; ++i)
            {
                destArr[i] = fromArr[i - offsetInDest];
            }
        }

        /// <summary>
        /// Gets the data subsection that the address lies within. If no appropriate section could be found,
        /// throws an ArgumentException.
        /// </summary>
        /// <param name="address">The address to lookup.</param>
        /// <returns>The corresponding data subsection that the address is in.</returns>
        private DataSubsection GetDataSegmentForAddress(int address)
        {
            bool foundSubsection = false;
            var subsection = default(DataSubsection);
            IEnumerator<DataSubsection> enumerator = m_DataSubsections.GetEnumerator();
            while (enumerator.MoveNext() && !foundSubsection)
            {
                subsection = enumerator.Current;
                if (subsection.IsAddressWithinSegment(address))
                {
                    foundSubsection = true;
                } 
            }

            if (!foundSubsection)
            {
                throw new ArgumentException("Address was out of range of .data area.");
            }

            return subsection;
        }

        /// <summary>
        /// Structure to quickly lookup data ranges, as well as the raw data itself.
        /// </summary>
        private struct DataSubsection
        {
            /// <summary>
            /// Creates a data subsection entry.
            /// </summary>
            /// <param name="startingOffset">The minimum address of this section.</param>
            /// <param name="limitOffset">The maximum address of this section.</param>
            /// <param name="data">An array of any pre-initialized data in this section.</param>
            public DataSubsection(int startingOffset, int limitOffset, byte[] data)
            {
                m_StartingOffset = startingOffset;
                RawData = data;
                m_LimitOffset = limitOffset;
            }

            /// <summary>
            /// Determines if the provided address falls within this segment.
            /// </summary>
            /// <param name="address">The address to check.</param>
            /// <returns>True if the address lies within this segment's address range; otherwise returns false.</returns>
            public bool IsAddressWithinSegment(int address)
            {
                return (m_StartingOffset <= address) && (address < m_LimitOffset);
            }

            /// <summary>
            /// Gets the starting (minimum) address of this segment.
            /// </summary>
            public int StartingAddress
            {
                get { return m_StartingOffset; }
            }


            public byte[] RawData;

            private readonly int m_StartingOffset;
            private readonly int m_LimitOffset;
        }

        private readonly List<DataSubsection> m_DataSubsections;
        
        // need to maintain control of the heap array, since we can reallocate this at will.
        // other areas not so much.
        private int m_CurrHeapAddress;
    }
}
