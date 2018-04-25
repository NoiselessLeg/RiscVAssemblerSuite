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
        /// <summary>
        /// Creates a register with a default value of zero.
        /// </summary>
        public Register()
        {
            m_Value = 0;
        }

        /// <summary>
        /// Creates a register with a provided value as its default value.
        /// </summary>
        /// <param name="defaultValue">The value to set the register to.</param>
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
