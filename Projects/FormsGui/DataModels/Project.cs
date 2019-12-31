using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.DataModels
{
   public class Project
   {
      public Project()
      {
         m_Files = new List<AssemblyFile>();
         m_Version = new ProjectFileVersion();
      }

      public Project(ProjectFileVersion version)
      {
         m_Version = version;
         m_Files = new List<AssemblyFile>();
      }

      public ProjectFileVersion Version
      {
         get { return m_Version; }
      }

      public string ProjectName
      {
         get { return m_ProjectName; }
         set
         {
            if (m_ProjectName != value)
            {
               m_ProjectName = value;
            }
         }
      }

      public string FilePath
      {
         get { return m_FilePath; }
         set
         {
            if (m_FilePath != value)
            {
               m_FilePath = value;
            }
         }
      }

      public List<AssemblyFile> ContainedFiles
      {
         get { return m_Files; }
      }

      private readonly ProjectFileVersion m_Version;
      private string m_ProjectName;
      private string m_FilePath;
      private readonly List<AssemblyFile> m_Files;
   }
}
