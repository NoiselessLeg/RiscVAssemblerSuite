#include "stdafx.h"
#include "ELF_Helper.h"
#include "ELF_Writer.h"
#include "elfio/elfio.hpp"
#include "MarshalUtils.h"

namespace Assembler
{
   namespace ELF_Wrapper
   {
      ELF_Writer::ELF_Writer() :
         m_pUnderlyingWriter(new ELFIO::elfio),
         m_pHelper(new ELF_Helper(*m_pUnderlyingWriter)),
         m_pDataBytes(nullptr),
         m_pTxtBytes(nullptr)
      {
         m_pUnderlyingWriter->create(ELFCLASS32, ELFDATA2LSB);
         m_pUnderlyingWriter->set_os_abi(ELFOSABI_NONE);
         m_pUnderlyingWriter->set_type(ET_EXEC);
         m_pUnderlyingWriter->set_machine(EM_RISCV);
      }

      ELF_Writer::~ELF_Writer()
      {
         this->!ELF_Writer();
      }

      ELF_Writer::!ELF_Writer()
      {
         delete m_pTxtBytes;
         delete m_pDataBytes;
         delete m_pHelper;
         delete m_pUnderlyingWriter;
         m_pTxtBytes = nullptr;
         m_pDataBytes = nullptr;
         m_pHelper = nullptr;
         m_pUnderlyingWriter = nullptr;
      }

      void ELF_Writer::AddDataSection(array<System::Byte>^ dataBytes, long address)
      {
         // marshal the managed byte array to an unmanaged char pointer (array)
         pin_ptr<System::Byte> pinnedBytes = &dataBytes[0]; 
         unsigned char* bytePtr = pinnedBytes; 
         const char* signedBytes = reinterpret_cast<const char*>(bytePtr);

         // create a "permanent" copy of this memory so it doesn't get corrupted
         // once we leave the scope (i.e. the pinning goes away).
         m_pDataBytes = new char[dataBytes->Length];
         memcpy(m_pDataBytes, signedBytes, dataBytes->Length);

         ELFIO::section* dataSection = m_pUnderlyingWriter->sections.add(".data");
         dataSection->set_type(SHT_PROGBITS);
         dataSection->set_flags(SHF_ALLOC | SHF_WRITE);

         //FIXME: need to figure out how to set alignment dynamically throughout segment.
         // for now be lazy and use word alignment.
         const ELFIO::Elf_Xword WORD_ALIGN = 0x4;
         dataSection->set_addr_align(WORD_ALIGN);
         dataSection->set_data(m_pDataBytes, dataBytes->Length);

         ELFIO::segment* dataSegment = m_pUnderlyingWriter->segments.add();
         dataSegment->set_type(PT_LOAD);
         dataSegment->set_virtual_address(address);
         dataSegment->set_physical_address(address);
         dataSegment->set_flags(PF_W | PF_R);
         dataSegment->set_align(0x10);
         dataSegment->add_section_index(dataSection->get_index(), dataSection->get_addr_align());
      }

      void ELF_Writer::AddSymbolTable(Assembler::Common::SymbolTable^ symTbl)
      {
         ELFIO::section* strTabSection = m_pUnderlyingWriter->sections.add(".strtab");
         strTabSection->set_type(SHT_STRTAB);
         strTabSection->set_flags(SHF_STRINGS);

         ELFIO::string_section_accessor strTable(strTabSection);
         ELFIO::section* symSection = m_pUnderlyingWriter->sections.add(".symtab");
         symSection->set_type(SHT_SYMTAB);
         strTabSection->set_flags(SHF_ALLOC);

         ELFIO::section* reloSection = m_pUnderlyingWriter->sections.add(".rela.txt");

         // strings of symbol names reside in the string table.
         int stringTableSectionIdx = m_pHelper->GetSectionIndex(".strtab");
         int relocationSectionIdx = m_pHelper->GetSectionIndex(".rela.txt");
         symSection->set_link(stringTableSectionIdx);
         ELFIO::symbol_section_accessor symbols(*m_pUnderlyingWriter, symSection);

         IEnumerator<Assembler::Common::Symbol^>^ enumerator = symTbl->Symbols->GetEnumerator();
         enumerator->Reset();
         while (enumerator->MoveNext())
         {
            Assembler::Common::Symbol^ symbol = enumerator->Current;
            std::string nativeSymName;
            MarshalUtils::MarshalString(symbol->LabelName, nativeSymName);

            // need to figure out which section header to point the symbol to.
            int sectionIdx = 0;
            int symSize = 0;
            int symType = STT_OBJECT;
            switch (symbol->SegmentType)
            {
               case Assembler::Common::SegmentType::Data:
               {
                  sectionIdx = m_pHelper->GetSectionIndex(".data");
                  break;
               }
               case Assembler::Common::SegmentType::Text:
               {
                  symType = STT_FUNC;
                  sectionIdx = m_pHelper->GetSectionIndex(".text");
                  break;
               }
            }

            symbols.add_symbol(strTable,
               nativeSymName.c_str(),
               symbol->Address,
               // FIXME: need to figure out the size of the underlying symbol
               symSize,
               // FIXME: need to change this to be dynamic (i.e. is this a function symbol, var symbol?)
               ELF_ST_INFO(STB_LOCAL, symType),
               STV_DEFAULT,
               sectionIdx);
         }

         const size_t SYM_TABLE_ENTRY_SIZE = sizeof(ELFIO::Elf32_Sym);

         // account for the null symbol table entry.
         symSection->set_info(symTbl->NumSymbols + 2);
         symSection->set_entry_size(SYM_TABLE_ENTRY_SIZE);

      }

      void ELF_Writer::AddTextSection(array<System::Byte>^ textBytes, long address)
      {
         // marshal the managed byte array to an unmanaged char pointer (array)
         pin_ptr<System::Byte> pinnedBytes = &textBytes[0]; 
         unsigned char* bytePtr = pinnedBytes; 
         const char* signedBytes = reinterpret_cast<const char*>(bytePtr);

         // create a "permanent" copy of this memory so it doesn't get corrupted
         // once we leave the scope (i.e. the pinning goes away).
         m_pTxtBytes = new char[textBytes->Length];
         memcpy(m_pTxtBytes, signedBytes, textBytes->Length);

         ELFIO::section* textSection = m_pUnderlyingWriter->sections.add(".text");
         textSection->set_type(SHT_PROGBITS);
         textSection->set_flags(SHF_ALLOC | SHF_EXECINSTR);

         //FIXME: need to figure out how to set alignment dynamically throughout segment.
         // for now be lazy and use word alignment.
         const ELFIO::Elf_Xword WORD_ALIGN = 0x10;
         textSection->set_addr_align(WORD_ALIGN);
         textSection->set_data(m_pTxtBytes, textBytes->Length);

         ELFIO::segment* textSegment = m_pUnderlyingWriter->segments.add();
         textSegment->set_type(PT_LOAD);
         textSegment->set_virtual_address(address);
         textSegment->set_physical_address(address);
         textSegment->set_flags(PF_X | PF_R);
         textSegment->set_align(0x1000);
         textSegment->add_section_index(textSection->get_index(), textSection->get_addr_align());

         m_pUnderlyingWriter->set_entry(address);
      }

      void ELF_Writer::WriteFile(System::String^ fileName)
      {
         std::string nativeStr;
         MarshalUtils::MarshalString(fileName, nativeStr);
         m_pUnderlyingWriter->save(nativeStr);
      }
   }
}
