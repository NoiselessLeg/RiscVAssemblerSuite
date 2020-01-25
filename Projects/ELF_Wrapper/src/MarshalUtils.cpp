#include "stdafx.h"
#include "MarshalUtils.h"

using namespace System::Runtime::InteropServices;

System::String^ MarshalUtils::MarshalString(const std::string& basicStr)
{
   return gcnew System::String(basicStr.c_str());
}

void MarshalUtils::MarshalString(System::String^ sysStr, std::string& basicStr)
{
   const char* chars = (const char*)(Marshal::StringToHGlobalAnsi(sysStr)).ToPointer();
   basicStr = chars;
   Marshal::FreeHGlobal(System::IntPtr((void*)chars));
}