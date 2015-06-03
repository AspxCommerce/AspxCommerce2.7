using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageFrame.ModuleLoader
{
    /// <summary>
    /// Business logic for module loader.
    /// </summary>
    public class ModuleLoaderController
    {
        /// <summary>
        /// Returns List of ModuleLoaderInfo for each ModuleName
        /// </summary>
        /// <param name="ModuleName"></param>
        /// <returns></returns>
        public List<ModuleLoaderInfo> GetControlSrcByModuleName(string ModuleName)
        {
            try
            {
                ModuleLoaderProvider objProvider = new ModuleLoaderProvider();
                return objProvider.GetControlSrcByModuleName(ModuleName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
