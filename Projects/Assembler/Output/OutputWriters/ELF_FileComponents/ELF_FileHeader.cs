using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.OutputWriters.ELF_FileComponents
{
   class ELF_FileHeader
   {
      public ELF_FileHeader(BasicObjectFile objFile)
      {
         m_Header = new Elf32_Header();
         m_Header.e_ident[0] = 0x7f;
         m_Header.e_ident[1] = 0x45;
         m_Header.e_ident[2] = 0x4c;
         m_Header.e_ident[3] = 0x46;

         //FIXME: need to add support for 32/64 bit stuff. should be able to be looked up in basicobjectfile.
         const byte PROGTYPE_32BIT = 1;
         m_Header.e_ident[4] = PROGTYPE_32BIT;

         // risc-v is always little-endian, so keep that.
         const byte ENDIANNESS_LITTLE_ENDIAN = 1;
         m_Header.e_ident[5] = ENDIANNESS_LITTLE_ENDIAN;

         const byte FORMAT_VERSION = 1;
         m_Header.e_ident[6] = FORMAT_VERSION;

         const byte ELFOSABI_NONE = 0;
         m_Header.e_ident[7] = ELFOSABI_NONE;

         // padding bytes.
         for (int i = 8; i < m_Header.e_ident.Length; ++i)
         {
            m_Header.e_ident[i] = 0;
         }

         const short EXECUTABLE_FILE_TYPE = 0x02;
         m_Header.e_type = EXECUTABLE_FILE_TYPE;
         
         const short RISCV_TARGET_ARCH = 0xF3;
         m_Header.e_machine = RISCV_TARGET_ARCH;

         const int FIXED_VERSION = 1;
         m_Header.e_version = FIXED_VERSION;

         m_Header.e_entry = Common.CommonConstants.BASE_TEXT_ADDRESS;

         m_Header.e_phoff = 0x40;
         m_Header.e_shoff = 0x40;
         m_Header.e_flags = 0;
         m_Header.e_ehsize = 0x40;

         
      }

      public void Write(Stream strm)
      {
         strm.Write(m_Header.e_ident, 0, m_Header.e_ident.Length);

         byte[] eTypeBytes = BitConverter.GetBytes(m_Header.e_type);
         strm.Write(eTypeBytes, 0, eTypeBytes.Length);
      }

      private class Elf32_Header
      {
         public Elf32_Header()
         {
            e_ident = new byte[16];
         }

         public readonly byte[] e_ident;
         public short e_type;
         public short e_machine;
         public int e_version;
         public int e_entry;
         public int e_phoff;
         public int e_shoff;
         public int e_flags;
         public short e_ehsize;
         public short e_phentsize;
         public short e_phnum;
         public short e_shentsize;
         public short e_shnum;
         public short e_shstrndx;

      }


      private readonly Elf32_Header m_Header;
   }
}
