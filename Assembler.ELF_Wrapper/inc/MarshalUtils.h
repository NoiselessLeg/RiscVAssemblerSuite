#pragma once
#include <string>

ref class MarshalUtils
{
public:
   static System::String^ MarshalString(const std::string& basicStr);
   static void MarshalString(System::String^ sysStr, std::string& basicStr);
};