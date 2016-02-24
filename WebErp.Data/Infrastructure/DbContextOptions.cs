using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Data.Infrastructure
{
    public class DbContextOptions: IDbContextOptions
    {
        public const string IN_MEMORY = "inMemory";
        public const string CONNECTION_NAME = "WebErp";
        public const string REQUIRE_UNIQUE_EMAIL = "RequireUniqueEmail";
        public const string RECREATE_DATABASE = "RecreateDatabase";

        private readonly bool _inMemory, _uniqueEmail, _recreateDatabase;
        private readonly string _nameOrConnectionString;

        public DbContextOptions(/*string nameOrConnectionString,bool inMemory,bool requireUniqueEmail*/)
        {
            this._nameOrConnectionString = ConfigurationManager.ConnectionStrings[DbContextOptions.CONNECTION_NAME]?.ConnectionString ?? DbContextOptions.CONNECTION_NAME;
            bool.TryParse(ConfigurationManager.AppSettings[IN_MEMORY], out _inMemory);
            bool.TryParse(ConfigurationManager.AppSettings[REQUIRE_UNIQUE_EMAIL], out _uniqueEmail);
            bool.TryParse(ConfigurationManager.AppSettings[RECREATE_DATABASE], out _recreateDatabase);
        }

        public string NameOrConnectionString => _nameOrConnectionString;

        public bool InMemory => _inMemory;

        public bool RequireUniqueEmail => _uniqueEmail;

        public bool RecreateDatabase => _recreateDatabase;

    }
}
