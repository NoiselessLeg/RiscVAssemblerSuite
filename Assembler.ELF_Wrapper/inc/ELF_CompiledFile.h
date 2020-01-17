#pragma once

namespace Assembler
{
   namespace ELF_Wrapper
   {
      ref class ELF_Reader;
   }
}

namespace Assembler
{
   namespace ELF_Wrapper
   {

      public ref class ELF_CompiledFile
      {
      public:
         ELF_CompiledFile(ELF_Reader^ readerInstance);
         property Assembler::Common::ReverseSymbolTable^ SymbolTable
         {
            Assembler::Common::ReverseSymbolTable^ get();
         }
         property System::Collections::Generic::IEnumerable<System::Byte>^ DataBytes
         {
            System::Collections::Generic::IEnumerable<System::Byte>^ get();
         }
         property System::Collections::Generic::IEnumerable<System::Int32>^ RawInstructions
         {
            System::Collections::Generic::IEnumerable<System::Int32>^ get();
         }

         property int DataSegmentStartingAddress
         {
            int get();
         }

         property int TextSegmentStartingAddress
         {
            int get();
         }

         property int DataSegmentSize
         {
            int get();
         }

         property int TextSegmentSize
         {
            int get();
         }

      private:
         Assembler::Common::ReverseSymbolTable^ m_SymTbl;
         System::Collections::Generic::IEnumerable<System::Byte>^ m_DataSection;
         System::Collections::Generic::IEnumerable<System::Int32>^ m_TxtSection;
         int m_DataSegAddr;
         int m_TextSegAddr;
         int m_DataSegSize;
         int m_TextSegSize;
      };
   }
}
