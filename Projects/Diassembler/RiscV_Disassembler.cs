using Assembler.Common;
using Assembler.Disassembler.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Disassembler
{
    public class RiscV_Disassembler
    {
        public bool DisassembleFile(string fileName, ILogger logger)
        {
            logger.Log(LogLevel.Info, "Invoking disassembler on file: " + fileName);

            using (var fileStrm = File.OpenRead(fileName))
            {
                using (var reader = new BinaryReader(fileStrm))
                {
                    bool foundTxtSection = false;
                    while (!reader.EndOfStream())
                    {
                        byte val = reader.ReadByte();
                        if (val != 0x2E)
                        {

                        }
                    }

                    if (!foundTxtSection)
                    {
                        logger.Log(LogLevel.Warning, "Not a valid .obj file produced by assembler.");
                    }
                }
            }

            return true;
        }
    }
}
