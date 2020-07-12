namespace Assembler.Interpreter
{
    /// <summary>
    /// Simulates a simple register.
    /// </summary>
    public class Register<TRegValue> : IRegister<TRegValue>
    {
        /// <summary>
        /// Creates a register with a default value of zero.
        /// </summary>
        public Register()
        {
            m_Value = default(TRegValue);
        }

        /// <summary>
        /// Creates a register with a provided value as its default value.
        /// </summary>
        /// <param name="defaultValue">The value to set the register to.</param>
        public Register(TRegValue defaultValue)
        {
            m_Value = defaultValue;
        }

        /// <summary>
        /// Gets or sets the value in the register.
        /// </summary>
        public virtual TRegValue Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        private TRegValue m_Value;
    }
}
