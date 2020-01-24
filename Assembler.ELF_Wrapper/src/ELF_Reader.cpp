#include "stdafx.h"
#include "ELF_Helper.h"
#include "ELF_Reader.h"
#include "elfio/elfio.hpp"
#include "MarshalUtils.h"

using namespace Assembler::Common;

namespace Assembler
{
   namespace ELF_Wrapper
   {
      ELF_Reader::ELF_Reader(System::String^ filePath):
         m_pUnderlyingReader(new ELFIO::elfio),
         m_pHelper(new ELF_Helper(*m_pUnderlyingReader))
      {
         std::string nativeStr;
         MarshalUtils::MarshalString(filePath, nativeStr);
         if (!m_pUnderlyingReader->load(nativeStr))
         {
            throw gcnew System::IO::IOException("Failed to load ELF file " + filePath);
         }

         if (m_pUnderlyingReader->get_machine() != EM_RISCV)
         {
            throw gcnew System::IO::InvalidDataException("ELF file \"" + filePath + "\" was not a valid RISC-V ELF file.");
         }
      }

      ELF_Reader::~ELF_Reader()
      {
         this->!ELF_Reader();
      }

      ELF_Reader::!ELF_Reader()
      {
         delete m_pHelper;
         m_pHelper = nullptr;
         delete m_pUnderlyingReader;
         m_pUnderlyingReader = nullptr;
      }

      IEnumerable<System::Byte>^ ELF_Reader::GenerateDataSegment()
      {
         using namespace System::Runtime::InteropServices;
         ELFIO::section* dataSection = m_pHelper->GetSectionByName(".data");
         if (!dataSection)
         {
            return gcnew array<System::Byte>(0);
         }

         const char* rawBytes = dataSection->get_data();
         size_t dataSegSize = dataSection->get_size();

         array<System::Byte>^ byteArr = gcnew array<System::Byte>(dataSegSize);
         Marshal::Copy(((System::IntPtr)((void*)rawBytes)), byteArr, 0, dataSegSize);

         return byteArr;
      }

      Assembler::Common::ReverseSymbolTable^ ELF_Reader::GenerateReverseSymbolTable()
      {
         ELFIO::section* symtbl = m_pHelper->GetSectionByType(SHT_SYMTAB);
         if (!symtbl)
         {
            return gcnew Assembler::Common::ReverseSymbolTable();
         }

         const ELFIO::symbol_section_accessor symbols(*m_pUnderlyingReader, symtbl);

         auto symTbl = gcnew Assembler::Common::ReverseSymbolTable();

         // start at one to ignore the null symbol
         for (uint32_t i = 1; i < symbols.get_symbols_num(); ++i)
         {
            std::string symName;
            ELFIO::Elf64_Addr symAddress = 0;
            ELFIO::Elf_Xword symSize = 0;
            unsigned char binding = 0;
            unsigned char type = 0;
            ELFIO::Elf_Half sectionIdx = 0;
            unsigned char other = 0;

            symbols.get_symbol(i, symName, symAddress, symSize, binding, type, sectionIdx, other);

            System::String^ convertedSymName = MarshalUtils::MarshalString(symName);
            SegmentType foundSegType = SegmentType::Invalid;
            ELFIO::section* sectionInstance = m_pHelper->GetSectionByIndex(sectionIdx);
            if (sectionInstance == nullptr)
            {
               throw gcnew System::InvalidProgramException("Could not find ELF section corresponding to section listed in symbol table. " +
                                                           "This indicates a bug in the assembler created an invalid program.");
            }

            std::string sectionName = sectionInstance->get_name();
            if (sectionName == ".data")
            {
               foundSegType = SegmentType::Data;
            }
            else if (sectionName == ".text")
            {
               foundSegType = SegmentType::Text;
            }
            else
            {
               _ASSERT(0);
            }

            auto symbol = gcnew Assembler::Common::Symbol(convertedSymName, foundSegType, symAddress);
            symbol->Size = symSize;
            symTbl->AddSymbol((int32_t)symAddress, symbol);
         }

         return symTbl;
      }

      IEnumerable<System::Int32>^ ELF_Reader::GenerateTextSegment()
      {
         using namespace System::Runtime::InteropServices;
         ELFIO::section* textSection = m_pHelper->GetSectionByName(".text");
         if (!textSection)
         {
            return gcnew array<System::Int32>(0);
         }

         const char* rawBytes = textSection->get_data();
         size_t textSegmentSize = textSection->get_size();

         if (textSegmentSize % sizeof(int) != 0)
         {
            throw gcnew System::InvalidOperationException("Size of .text segment is not divisible by word size.");
         }

         // divide this by four. the segment size is in bytes, so we need
         // to get the size in words.
         size_t arraySize = textSegmentSize / 4;

         array<System::Int32>^ wordArr = gcnew array<System::Int32>(arraySize);
         Marshal::Copy(((System::IntPtr)((void*)rawBytes)), wordArr, 0, arraySize);

         return wordArr;
      }

      int ELF_Reader::GetDataSegmentSize()
      {
         int size = 0;
         ELFIO::section* dataSection = m_pHelper->GetSectionByName(".data");
         if (dataSection)
         {
            size = dataSection->get_size();
         }

         return size;
      }

      int ELF_Reader::GetTextSegmentSize()
      {
         int size = 0;
         ELFIO::section* textSection = m_pHelper->GetSectionByName(".text");
         if (!textSection)
         {
            size = textSection->get_size();
         }

         return size;
      }

      int ELF_Reader::GetDataSegmentStartingAddress()
      {
         ELFIO::segment* dataSeg = m_pHelper->GetSegmentByName(".data");
         int startingAddr = 0;
         if (dataSeg)
         {
            startingAddr = dataSeg->get_physical_address();
         }

         return startingAddr;
      }

      int ELF_Reader::GetTextSegmentStartingAddress()
      {
         int size = 0;
         ELFIO::segment* textSeg = m_pHelper->GetSegmentByName(".text");
         if (textSeg)
         {
            size = textSeg->get_physical_address();
         }
         return size;
      }
   }
}