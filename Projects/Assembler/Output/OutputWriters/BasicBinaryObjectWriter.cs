using Assembler.Output.ObjFileComponents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.OutputWriters
{
    class BasicBinaryObjectWriter : IObjectFileWriter
    {

        public void WriteObjectFile(string fileName, BasicObjectFile file)
        {
            FileStream fs = File.Open(fileName, FileMode.Create);
            // write the .data preamble
            fs.Write(Encoding.ASCII.GetBytes(".data"), 0, Encoding.ASCII.GetByteCount(".data"));
            foreach (IObjectFileComponent elem in file.DataElements)
            {
                elem.WriteDataToFile(fs);
            }

            // write the .text preamble
            fs.Write(Encoding.ASCII.GetBytes(".text"), 0, Encoding.ASCII.GetByteCount(".text"));
            foreach (IObjectFileComponent elem in file.TextElements)
            {
                elem.WriteDataToFile(fs);
            }

            // write the symbol table
            fs.Write(Encoding.ASCII.GetBytes(".symtbl"), 0, Encoding.ASCII.GetByteCount(".symtbl"));
            foreach (Symbol elem in file.SymbolTable.Symbols)
            {
                fs.Write(Encoding.ASCII.GetBytes(elem.LabelName), 0, Encoding.ASCII.GetByteCount(elem.LabelName));
                byte[] byteRepresentation = BitConverter.GetBytes(elem.Address);
                fs.Write(byteRepresentation, 0, byteRepresentation.Length);
            }
        }
        
    }
}
