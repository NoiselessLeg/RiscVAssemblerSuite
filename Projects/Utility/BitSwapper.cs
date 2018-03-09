namespace Assembler.Common
{
    class BitSwapper
    {
        /// <summary>
        /// Swaps all bits in a uint value (e.g. 0x1 would become 0x8)
        /// </summary>
        /// <param name="value">The value whose bits shall be swapped.</param>
        /// <returns>The bitswapped version of value.</returns>
        public static uint SwapBits(uint value)
        {
            uint swappedVal = 0;
            for (int i = 0; i < sizeof(uint); ++i)
            {
                // essentially mask each bit starting from the right.
                // shift that bit to its appropriate position starting on the left side.
                // (e.g. 2 will be masked and moved to the 30th position from the right.
                swappedVal |= ((0x1u << i) & value) << (sizeof(uint) - 1 - i);
            }

            return swappedVal;
        }

        /// <summary>
        /// Swaps all bits in a uint value (e.g. 0x1 would become 0x8)
        /// </summary>
        /// <param name="value">The value whose bits shall be swapped.</param>
        /// <returns>The bitswapped version of value.</returns>
        public static int SwapBits(int value)
        {
            return (int)SwapBits((uint)value);
        }
    }
}
