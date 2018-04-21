﻿using Assembler.Common;
using Assembler.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine
{
    class ConsoleTerminal : ITerminal
    {
        public ConsoleTerminal(IRuntimeEnvironment parentEnvironment)
        {
            m_Environment = parentEnvironment;
        }

        public byte[] AllocMemory()
        {
            return m_Environment.AllocMemory();
        }

        public void PrintChar(char c)
        {
            Console.Write(c);
        }

        public void PrintInt(int value)
        {
            Console.Write(value);
        }

        public void PrintString(string value)
        {
            Console.Write(value);
        }

        public char ReadChar()
        {
            string value = Console.ReadLine();
            return char.Parse(value);
        }

        public int ReadInt()
        {
            string value = Console.ReadLine();
            return int.Parse(value);
        }

        public string ReadString()
        {
            return Console.ReadLine();
        }

        public void Terminate()
        {
            m_Environment.Terminate();
        }

        private readonly IRuntimeEnvironment m_Environment;
    }
}
