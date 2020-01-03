using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.IO
{
   public class SettingsFileSaver
   {
      public SettingsFileSaver()
      {
         m_ParamDic = new Dictionary<string, Dictionary<string, string>>();
      }

      public void AddParameter<T>(string paramGrp, string paramName, T paramValue)
      {
         Dictionary<string, string> grpTable;
         if (m_ParamDic.TryGetValue(paramGrp, out grpTable))
         {
            grpTable[paramName] = paramValue.ToString();
         }
         else
         {
            grpTable = new Dictionary<string, string>()
            {
               { paramName, paramValue.ToString() }
            };

            m_ParamDic.Add(paramGrp, grpTable);

         }
      }

      public void CommitToExternalStorage(string fileName)
      {
         using (var outputStrm = new FileStream(fileName, FileMode.Create))
         {
            using (var writer = new StreamWriter(outputStrm))
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


      private readonly Dictionary<string, Dictionary<string, string>> m_ParamDic;
   }
}
