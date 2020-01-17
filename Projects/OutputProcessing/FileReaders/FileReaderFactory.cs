using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.FileReaders
{
   public class FileReaderFactory
   {
      public FileReaderFactory()
      {
         m_FileParsers = new Dictionary<CompiledFileTypes, ICompiledFileReader>()
         {
            { CompiledFileTypes.JefFile, new JefFileReader() },
            { CompiledFileTypes.ElfFile, new ElfFileReader() }
         };
      }

      public ICompiledFileReader GetFileParser(string filePath)
      {
         CompiledFileTypes fileType = CompiledFileTypes.Invalid;
         if (filePath.EndsWith(".jef"))
         {
            fileType = CompiledFileTypes.JefFile;
         }
         else
         {
            fileType = CompiledFileTypes.ElfFile;
         }

         return GetFileParser(fileType);
      }

      private ICompiledFileReader GetFileParser(CompiledFileTypes fileType)
      {
         if (!m_FileParsers.TryGetValue(fileType, out ICompiledFileReader reader))
         {
            throw new ArgumentException("No file reader found for file type.");
         }

         return reader;
      }

      private enum CompiledFileTypes
      {
         Invalid,
         JefFile,
         ElfFile
      }

      private readonly Dictionary<CompiledFileTypes, ICompiledFileReader> m_FileParsers;
   }
}
