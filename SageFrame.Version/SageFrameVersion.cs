using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Version
{
    public class SageFrameVersion
    {
        public System.Version Version
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        public string FormatShortVersion(System.Version version, bool includeBuild)
        {

            string strVersion = (version.Major.ToString("0") + ("." + (version.Minor.ToString("0"))));
            return strVersion;
        }

        public string FormatVersion(System.Version version, bool includeBuild)
        {
            string strVersion = (version.Major.ToString("00") + ("." + (version.Minor.ToString("00") + ("." + version.Build.ToString("00")))));
            if (includeBuild)
            {
                strVersion = (strVersion + (" (" + (version.Revision.ToString() + ")")));
            }
            return strVersion;
        }
    }
}
