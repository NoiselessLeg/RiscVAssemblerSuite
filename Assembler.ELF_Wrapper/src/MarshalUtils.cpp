#include "stdafx.h"
#include "MarshalUtils.h"

using namespace System::Runtime::InteropServices;

void MarshalUtils::MarshalString(System::String^ sysStr, std::string& basicStr)
{
   const char* chars = (const char*)(Marshal::StringToHGlobalAnsi(sysStr)).ToPointer();
   basicStr = chars;
   Marshal::FreeHGlobal(System::IntPtr((void*)chars));
}