#include "stdafx.h"
#include "ELF_CompiledFile.h"
#include "ELF_Reader.h"

namespace Assembler
{
   namespace ELF_Wrapper
   {
      ELF_CompiledFile::ELF_CompiledFile(ELF_Reader^ readerInstance) :
         m_SymTbl(readerInstance->GenerateReverseSymbolTable()),
         m_DataSection(readerInstance->GenerateDataSegment()),
         m_TxtSection(readerInstance->GenerateTextSegment()),
         m_DataSegAddr(readerInstance->GetDataSegmentStartingAddress()),
         m_TextSegAddr(readerInstance->GetTextSegmentStartingAddress()),
         m_DataSegSize(readerInstance->GetDataSegmentSize()),
         m_TextSegSize(readerInstance->GetTextSegmentSize())
      {
      }

      Assembler::Common::ReverseSymbolTable^ ELF_CompiledFile::SymbolTable::get()
      {
         return m_SymTbl;
      }

      System::Collections::Generic::IEnumerable<System::Byte>^ ELF_CompiledFile::DataBytes::get()
      {
         return m_DataSection;
      }

      System::Collections::Generic::IEnumerable<System::Int32>^ ELF_CompiledFile::RawInstructions::get()
      {
         return m_TxtSection;
      }

      int ELF_CompiledFile::DataSegmentStartingAddress::get()
      {
         return m_DataSegAddr;
      }

      int ELF_CompiledFile::TextSegmentStartingAddress::get()
      {
         return m_TextSegAddr;
      }

      int ELF_CompiledFile::DataSegmentSize::get()
      {
         return m_DataSegSize;
      }

      int ELF_CompiledFile::TextSegmentSize::get()
      {
         return m_TextSegSize;
      }
   }
}
