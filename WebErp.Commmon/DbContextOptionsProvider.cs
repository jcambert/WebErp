using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Hosting;
using WebErp.Data.Infrastructure;

namespace WebErp.Commmon
{
    public class DbContextOptionsProvider : Provider<IDbContextOptions>
    {
        protected override IDbContextOptions CreateInstance(IContext context)
        {
            Configuration rootCfg;
            bool _inMemory;
            if (HostingEnvironment.IsHosted)
                rootCfg = WebConfigurationManager.OpenWebConfiguration(null);
            else
                rootCfg = ConfigurationManager.OpenExeConfiguration(null);

            bool.TryParse(rootCfg.AppSettings.Settings[DbContextOptions.IN_MEMORY]?.Value, out _inMemory);
            string _connectionName = rootCfg.ConnectionStrings.ConnectionStrings[DbContextOptions.CONNECTION_NAME]?.Name ?? DbContextOptions.CONNECTION_NAME;
            return new DbContextOptions(_connectionName, _inMemory);
        }
    }
}
