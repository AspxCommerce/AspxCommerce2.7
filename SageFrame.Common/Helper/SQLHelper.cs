using SageFrame.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageFrame.Common
{
    public class SQLHelper
    {
        public void ExecuteModuleDataCleanupScript()
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                SQLH.ExecuteNonQuery("[dbo].[usp_sagecleanup]");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
