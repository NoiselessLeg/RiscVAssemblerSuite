#pragma once

using namespace System::Collections::Generic;

namespace ELFIO
{
   class elfio;
   class section;
}

namespace Assembler
{
   namespace ELF_Wrapper
   {
      class ELF_Helper;
      public ref class ELF_Reader
      {
      public:
         ELF_Reader(System::String^ filePath);

         ~ELF_Reader();

         !ELF_Reader();

         IEnumerable<System::Byte>^ GenerateDataSegment();
         Assembler::Common::ReverseSymbolTable^ GenerateReverseSymbolTable();
         IEnumerable<System::Int32>^ GenerateTextSegment();

         int GetDataSegmentSize();
         int GetTextSegmentSize();

         int GetDataSegmentStartingAddress();
         int GetTextSegmentStartingAddress();


      private:

         ELFIO::elfio* m_pUnderlyingReader;
         ELF_Helper* m_pHelper;
         char* m_pDataBytes;
         char* m_pTxtBytes;
      };
   }
}