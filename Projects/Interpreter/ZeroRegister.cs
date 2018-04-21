using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter
{
    class ZeroRegister : Register
    {
        public ZeroRegister() :
            base(0)
        {
        }

        public ZeroRegister(int defaultValue) :
            base(0)
        {
        }

        /// <summary>
        /// Force the value to always be zero in the zero register.
        /// </summary>
        public override int Value
        {
            get
            {
                return base.Value;
            }

            set
            {
                base.Value = 0;
            }
        }
    }
}
