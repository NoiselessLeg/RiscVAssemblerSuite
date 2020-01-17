#pragma once

namespace ELFIO
{
   class elfio;
   class section;
   class segment;
}

namespace Assembler
{
   namespace ELF_Wrapper
   {
      class ELF_Helper
      {
      public:
         ELF_Helper(const ELFIO::elfio& libInstance);
         ELFIO::section* GetSectionByType(long type) const;
         ELFIO::section* GetSectionByName(const char* name) const;
         ELFIO::section* GetSectionByIndex(int index) const;
         int GetSectionIndex(const char* sectionName) const;
         ELFIO::segment* GetSegmentByName(const char* name) const;

      private:
         const ELFIO::elfio& m_Instance;
      };
   }
}