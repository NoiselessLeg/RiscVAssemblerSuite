﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    /// <summary>
    /// Defines a class that accepts user input, and can output data to the screen.
    /// </summary>
    public interface ITerminal
    {
        /// <summary>
        /// Prints an integer to a terminal window.
        /// </summary>
        /// <param name="value">The value that shall be printed by the implementation.</param>
        void PrintInt(int value);

        /// <summary>
        /// Prints a string to the terminal window.
        /// </summary>
        /// <param name="value">The string that shall be printed by the implementation.</param>
        void PrintString(string value);

        /// <summary>
        /// Reads an integer provided by the user.
        /// </summary>
        /// <returns>The 32-bit integer that was read from the terminal.</returns>
        int ReadInt();

        /// <summary>
        /// Reads a string from the terminal.
        /// </summary>
        /// <returns>The string read from the terminal.</returns>
        string ReadString();

        /// <summary>
        /// Terminates the application
        /// </summary>
        void Terminate();

        /// <summary>
        /// Prints a character to the terminal.
        /// </summary>
        /// <param name="c">The character to print.</param>
        void PrintChar(char c);

        /// <summary>
        /// Reads a character from the terminal.
        /// </summary>
        /// <returns>The character that was read.</returns>
        char ReadChar();
    }
}
