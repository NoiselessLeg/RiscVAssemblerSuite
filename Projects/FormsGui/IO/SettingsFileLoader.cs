using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.IO
{
   public class SettingsFileLoader
   {
      public SettingsFileLoader(string fileName)
      {
         m_ParamDic = new Dictionary<string, string>();

         try
         {
            string file = string.Empty;
            using (var fileStrm = new FileStream(fileName, FileMode.Open))
            {
               using (var reader = new StreamReader(fileStrm))
               {
                  file = reader.ReadToEnd();
               }
            }
            
            file = file.RemoveInstancesOf(Environment.NewLine);
            file = file.RemoveInstancesOf('\n');
            string[] allOpts = file.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string param in allOpts)
            {
               string[] keyValPair = param.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
               if (keyValPair.Length != 2)
               {
                  throw new ArgumentException("Failed to parse settings file. A new file will be created.");
               }
               m_ParamDic.Add(keyValPair[0], keyValPair[1]);
            }

         }
         catch (FileNotFoundException)
         {
         }
         catch (Exception)
         {
            File.Delete(fileName);
            throw;
         }
      }

      public T GetParameter<T>(string param)
      {
         T val = default(T);
         if (m_ParamDic.TryGetValue(param, out string strVal))
         {
            val = (T) Convert.ChangeType(strVal, typeof(T));
         }
         return val;
      }

      private readonly Dictionary<string, string> m_ParamDic;
   }
}
