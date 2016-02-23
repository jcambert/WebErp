using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Ninject.Activation;
using System.Configuration;
using System.Web.Configuration;
using WebErp.Data;

namespace WebErp
{
    public static class DatabaseOptions
    {
        public const string USE_IN_MEMORY = "useInMemory";
        public const string CONNECTION_NAME = "WebErp";
        public static bool UseInMemory => _useInMemory;
        public static string ConnectionString => _connectionString;
        private static bool _useInMemory;
        private static string _connectionString;
        static DatabaseOptions()
        {
            Configuration cfg = WebConfigurationManager.OpenWebConfiguration(null);
            var use = cfg.AppSettings.Settings[USE_IN_MEMORY]?.Value;
            _connectionString = cfg.ConnectionStrings.ConnectionStrings[CONNECTION_NAME].ConnectionString;

            bool.TryParse(use, out _useInMemory);

        }
        public static DbContextOptions Options(IContext context)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebErpContext>();

            if (UseInMemory)
                optionsBuilder.UseInMemoryDatabase();
            else
                optionsBuilder.UseSqlServer(ConnectionString);
            return optionsBuilder.Options;

        }

    }
}