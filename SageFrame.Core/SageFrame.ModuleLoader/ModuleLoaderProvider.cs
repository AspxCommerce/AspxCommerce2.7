using SageFrame.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SageFrame.ModuleLoader
{
    /// <summary>
    ///  Manupulates data for module loader.
    /// </summary>
    public class ModuleLoaderProvider
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
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleName", ModuleName));
                return SQLH.ExecuteAsList<ModuleLoaderInfo>("[dbo].[sp_GetControlSrcByModuleName]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
