using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.DataModels
{
    public class ProjectFileVersion
    {
        public ProjectFileVersion():
            this(DefaultVersions.DefaultMajorVersion, DefaultVersions.DefaultMinorVersion)
        {

        }

        public ProjectFileVersion(int major, int minor)
        {
            m_MajorVersion = major;
            m_MinorVersion = minor;
        }

        public int MajorVersion
        {
            get { return m_MajorVersion; }
        }

        public int MinorVersion
        {
            get { return m_MinorVersion; }
        }

        public static bool operator == (ProjectFileVersion lhs, ProjectFileVersion rhs)
        {
            return lhs.MajorVersion == rhs.MajorVersion &&
                   rhs.MinorVersion == rhs.MinorVersion;
        }

        public static bool operator != (ProjectFileVersion lhs, ProjectFileVersion rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            return (this == (ProjectFileVersion)obj);
        }

        public override int GetHashCode()
        {
            return MajorVersion.GetHashCode() + MinorVersion.GetHashCode();
        }

        private readonly int m_MajorVersion;
        private readonly int m_MinorVersion;
    }
}
