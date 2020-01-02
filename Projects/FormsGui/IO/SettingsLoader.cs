using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.IO
{
   public class SettingsLoader
   {
      public SettingsLoader(string fileName)
      {
         m_KeyValueDic = new Dictionary<string, string>();
         string fileTxt = string.Empty;
         using (var fs = new FileStream(fileName, FileMode.Open))
         {
            using (var reader = new StreamReader(fs))
            {
               fileTxt = reader.ReadToEnd();
            }
         }

         // get rid of any newline type before splitting
         fileTxt = fileTxt.Replace(Environment.NewLine, string.Empty);
         string[] optArr = fileTxt.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

         foreach (string option in optArr)
         {
            string[] kvp = option.Split('=');
            if (kvp.Length != 2)
            {
               throw new ArgumentException("Preference file was not well-formed.");
            }

            m_KeyValueDic.Add(kvp[0], kvp[1]);
         }
      }

      public T GetParameter<T>(string keyName)
      {
         if (m_KeyValueDic.TryGetValue(keyName, out string value))
         {
            return (T) Convert.ChangeType(value, typeof(T));
         }
         else
         {
            throw new ArgumentException("Key \"" + keyName + "\" was not found in settings file.");
         }
      }

      private readonly Dictionary<string, string> m_KeyValueDic;
   }
}
