#pragma once

struct Dwarf_Debug;

public ref class DWARF_Wrapper
{
public:
   DWARF_Wrapper();
   ~DWARF_Wrapper();
   !DWARF_Wrapper();

private:
   Dwarf_Debug* m_pDwarfObj;
};