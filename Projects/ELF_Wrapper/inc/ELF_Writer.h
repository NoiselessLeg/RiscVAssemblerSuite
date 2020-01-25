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

      public ref class ELF_Writer
      {
      public:
         ELF_Writer();

         ~ELF_Writer();

         !ELF_Writer();

         void AddDataSection(array<System::Byte>^ dataBytes, long address);
         void AddSymbolTable(Assembler::Common::SymbolTable^ symTbl);
         void AddTextSection(array<System::Byte>^ textBytes, long address);

         void WriteFile(System::String^ fileName);

      private:

         ELFIO::elfio* m_pUnderlyingWriter;
         ELF_Helper* m_pHelper;
         char* m_pDataBytes;
         char* m_pTxtBytes;
      };
   }
}