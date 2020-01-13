#pragma once
#include <string>

ref class MarshalUtils
{
public:
   static void MarshalString(System::String^ sysStr, std::string& basicStr);
};