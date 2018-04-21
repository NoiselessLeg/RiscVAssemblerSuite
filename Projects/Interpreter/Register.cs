using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter
{
    /// <summary>
    /// Simulates a simple 32-bit register.
    /// </summary>
    public class Register
    {
        public Register()
        {
            m_Value = 0;
        }

        public Register(int defaultValue)
        {
            m_Value = defaultValue;
        }

        /// <summary>
        /// Gets or sets the value in the register.
        /// </summary>
        public virtual int Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        private int m_Value;
    }
}
