#include "stdafx.h"
#include "elfio/elfio.hpp"
#include "ELF_Helper.h"

namespace Assembler
{
   namespace ELF_Wrapper
   {
      ELF_Helper::ELF_Helper(const ELFIO::elfio& instance) :
         m_Instance(instance)
      {
      }

      ELFIO::section* ELF_Helper::GetSectionByType(long type) const
      {
         ELFIO::section* targetSec = nullptr;
         for (auto secItr = m_Instance.sections.begin();
              secItr != m_Instance.sections.end();
              ++secItr)
         {
            ELFIO::section* section = *secItr;
            if (section->get_type() == type)
            {
               targetSec = section;
               break;
            }
         }

         return targetSec;
      }

      ELFIO::section* ELF_Helper::GetSectionByName(const char* name) const
      {
         ELFIO::section* targetSec = nullptr;
         for (auto secItr = m_Instance.sections.begin();
              secItr != m_Instance.sections.end();
              ++secItr)
         {
            ELFIO::section* section = *secItr;
            if (section->get_name() == name)
            {
               targetSec = section;
               break;
            }
         }

         return targetSec;
      }

      int ELF_Helper::GetSectionIndex(const char* sectionName) const
      {
         int sectionIdx = -1;
         for (auto secItr = m_Instance.sections.begin();
              secItr != m_Instance.sections.end();
              ++secItr)
         {
            ELFIO::section* section = *secItr;
            if (section->get_name() == sectionName)
            {
               sectionIdx = section->get_index();
               break;
            }
         }

         return sectionIdx;
      }

      ELFIO::section* ELF_Helper::GetSectionByIndex(int sectionIdx) const
      {
         return m_Instance.sections[sectionIdx];
      }


      ELFIO::segment* ELF_Helper::GetSegmentByName(const char* name) const
      {
         bool found = false;
         ELFIO::segment* targetSeg = nullptr;
         for (auto itr = m_Instance.segments.begin(); itr != m_Instance.segments.end() && !found; ++itr)
         {
            ELFIO::segment* currSeg = *itr;
            if (currSeg->get_sections_num() > 0)
            {
               int secIdx = currSeg->get_section_index_at(0);

               if (m_Instance.sections[secIdx]->get_name() == name)
               {
                  found = true;
                  targetSeg = currSeg;
               }
            }
         }

         return targetSeg;
      }
   }
}