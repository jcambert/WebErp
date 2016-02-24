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
   /* public class DbContextOptionsProvider : Provider<IDbContextOptions>
    {
       

        public DbContextOptionsProvider()
        {
         
        }
        protected override IDbContextOptions CreateInstance(IContext context)
        {
            bool _inMemory, _uniqueEmail;
            bool.TryParse(ConfigurationManager.AppSettings[DbContextOptions.IN_MEMORY], out _inMemory);
            bool.TryParse(ConfigurationManager.AppSettings[DbContextOptions.REQUIRE_UNIQUE_EMAIL], out _uniqueEmail);
            string _connectionName = ConfigurationManager.ConnectionStrings[DbContextOptions.CONNECTION_NAME]?.ConnectionString ?? DbContextOptions.CONNECTION_NAME;
            return new DbContextOptions(_connectionName, _inMemory,_uniqueEmail);
        }
    }*/
}
