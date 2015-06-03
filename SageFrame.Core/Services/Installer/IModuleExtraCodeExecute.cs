using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SageFrame.Core.Services
{
    /// <summary>
    /// Interface for installation.
    /// </summary>
    public interface IModuleExtraCodeExecute
    {
        void ExecuteOnInstallation(XmlDocument doc, string tempFolderPath);

        void ExecuteOnUnInstallation(XmlDocument doc);
    }
}
