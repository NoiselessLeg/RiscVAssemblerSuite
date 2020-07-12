using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   public static class CommonConstants
   {

      public const int BASE_TEXT_ADDRESS = 0x00400000;
      public const int BASE_DATA_ADDRESS = 0x10010000;
      public const int BASE_EXTERN_ADDRESS = 0x10000000;
      public const int BASE_INSTRUCTION_SIZE_BYTES = 0x04;
      public const int DEFAULT_ALIGNMENT = 0x04;
      public const int DEFAULT_STACK_ADDRESS = 0x7fffeffc;
      public const int CANONICAL_NAN = 0x7fc00000;
   }
}
