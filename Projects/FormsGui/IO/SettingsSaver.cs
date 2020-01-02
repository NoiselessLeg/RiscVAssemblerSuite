using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.IO
{
   public class SettingsSaver
   {
      public SettingsSaver()
      {
         m_ParamDic = new Dictionary<string, Dictionary<string, string>>();
      }

      public void CommitSettingsToFile(string fileName)
      {
         using (var fs = new FileStream(fileName, FileMode.Create))
         {
            using (var writer = new StreamWriter(fs))
            {
               foreach (var paramGrp in m_ParamDic)
               {
                  foreach (var param in paramGrp.Value)
                  {
                     writer.WriteLine(param.Key + '=' + param.Value + ';');
                  }
               }
            }
         }
      }

      /// <summary>
      /// Commits the header, parameterName, and value to long-term storage.
      /// Note that this does not guarantee the file is committed to storage.
      /// CommitSettingsToFile must be called to ensure the system attempts
      /// to 
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="headerName"></param>
      /// <param name="parameterName"></param>
      /// <param name="value"></param>
      public void WriteSettingsData<T>(string headerName, string parameterName, T value)
      {
         if (m_ParamDic.TryGetValue(headerName, out Dictionary<string, string> settingsGrp))
         {
            settingsGrp[parameterName] = value.ToString();
         }
         else
         {
            m_ParamDic.Add(headerName, new Dictionary<string, string>());
            m_ParamDic[headerName][parameterName] = value.ToString();
         }

      }

      private Dictionary<string, Dictionary<string, string>> m_ParamDic;

   }
}
