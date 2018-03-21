

using Assembler.Common;
using System;
using System.IO;

// Note this file is auto-generated.
// Changes to the .cs file may be overwritten upon save.
// Make changes to the .tt file.


namespace Assembler.Output.ObjFileComponents
{
    /// <summary>
/// Represents a Int16 as a data element in a .obj file.
/// </summary>
public class Int16DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        /// <param name="targetEndianness">The target output endianness.</param>
        public Int16DataElement(Int16 elem, Endianness targetEndianness)
        {
            m_Elem = elem;
            m_TargetEndianness = targetEndianness;
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem, m_TargetEndianness);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the provided Int16 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(Int16 param, Endianness targetEndianness)
        {
            byte[] byteRep = BitConverter.GetBytes(param);

            // if the architecture we're assembling on is not our desired endianness,
            // flip the byte array.
            if (BitConverter.IsLittleEndian && targetEndianness == Endianness.BigEndian ||
                !BitConverter.IsLittleEndian && targetEndianness == Endianness.LittleEndian)
            {
                Array.Reverse(byteRep);
            }
            return byteRep;
        }

        private readonly Int16 m_Elem;
        private readonly Endianness m_TargetEndianness;
    }

    /// <summary>
/// Represents a UInt16 as a data element in a .obj file.
/// </summary>
public class UInt16DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        /// <param name="targetEndianness">The target output endianness.</param>
        public UInt16DataElement(UInt16 elem, Endianness targetEndianness)
        {
            m_Elem = elem;
            m_TargetEndianness = targetEndianness;
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem, m_TargetEndianness);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the provided UInt16 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(UInt16 param, Endianness targetEndianness)
        {
            byte[] byteRep = BitConverter.GetBytes(param);

            // if the architecture we're assembling on is not our desired endianness,
            // flip the byte array.
            if (BitConverter.IsLittleEndian && targetEndianness == Endianness.BigEndian ||
                !BitConverter.IsLittleEndian && targetEndianness == Endianness.LittleEndian)
            {
                Array.Reverse(byteRep);
            }
            return byteRep;
        }

        private readonly UInt16 m_Elem;
        private readonly Endianness m_TargetEndianness;
    }

    /// <summary>
/// Represents a Int32 as a data element in a .obj file.
/// </summary>
public class Int32DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        /// <param name="targetEndianness">The target output endianness.</param>
        public Int32DataElement(Int32 elem, Endianness targetEndianness)
        {
            m_Elem = elem;
            m_TargetEndianness = targetEndianness;
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem, m_TargetEndianness);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the provided Int32 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(Int32 param, Endianness targetEndianness)
        {
            byte[] byteRep = BitConverter.GetBytes(param);

            // if the architecture we're assembling on is not our desired endianness,
            // flip the byte array.
            if (BitConverter.IsLittleEndian && targetEndianness == Endianness.BigEndian ||
                !BitConverter.IsLittleEndian && targetEndianness == Endianness.LittleEndian)
            {
                Array.Reverse(byteRep);
            }
            return byteRep;
        }

        private readonly Int32 m_Elem;
        private readonly Endianness m_TargetEndianness;
    }

    /// <summary>
/// Represents a UInt32 as a data element in a .obj file.
/// </summary>
public class UInt32DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        /// <param name="targetEndianness">The target output endianness.</param>
        public UInt32DataElement(UInt32 elem, Endianness targetEndianness)
        {
            m_Elem = elem;
            m_TargetEndianness = targetEndianness;
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem, m_TargetEndianness);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the provided UInt32 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(UInt32 param, Endianness targetEndianness)
        {
            byte[] byteRep = BitConverter.GetBytes(param);

            // if the architecture we're assembling on is not our desired endianness,
            // flip the byte array.
            if (BitConverter.IsLittleEndian && targetEndianness == Endianness.BigEndian ||
                !BitConverter.IsLittleEndian && targetEndianness == Endianness.LittleEndian)
            {
                Array.Reverse(byteRep);
            }
            return byteRep;
        }

        private readonly UInt32 m_Elem;
        private readonly Endianness m_TargetEndianness;
    }

    /// <summary>
/// Represents a Int64 as a data element in a .obj file.
/// </summary>
public class Int64DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        /// <param name="targetEndianness">The target output endianness.</param>
        public Int64DataElement(Int64 elem, Endianness targetEndianness)
        {
            m_Elem = elem;
            m_TargetEndianness = targetEndianness;
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem, m_TargetEndianness);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the provided Int64 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(Int64 param, Endianness targetEndianness)
        {
            byte[] byteRep = BitConverter.GetBytes(param);

            // if the architecture we're assembling on is not our desired endianness,
            // flip the byte array.
            if (BitConverter.IsLittleEndian && targetEndianness == Endianness.BigEndian ||
                !BitConverter.IsLittleEndian && targetEndianness == Endianness.LittleEndian)
            {
                Array.Reverse(byteRep);
            }
            return byteRep;
        }

        private readonly Int64 m_Elem;
        private readonly Endianness m_TargetEndianness;
    }

    /// <summary>
/// Represents a UInt64 as a data element in a .obj file.
/// </summary>
public class UInt64DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        /// <param name="targetEndianness">The target output endianness.</param>
        public UInt64DataElement(UInt64 elem, Endianness targetEndianness)
        {
            m_Elem = elem;
            m_TargetEndianness = targetEndianness;
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem, m_TargetEndianness);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the provided UInt64 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(UInt64 param, Endianness targetEndianness)
        {
            byte[] byteRep = BitConverter.GetBytes(param);

            // if the architecture we're assembling on is not our desired endianness,
            // flip the byte array.
            if (BitConverter.IsLittleEndian && targetEndianness == Endianness.BigEndian ||
                !BitConverter.IsLittleEndian && targetEndianness == Endianness.LittleEndian)
            {
                Array.Reverse(byteRep);
            }
            return byteRep;
        }

        private readonly UInt64 m_Elem;
        private readonly Endianness m_TargetEndianness;
    }

    }
