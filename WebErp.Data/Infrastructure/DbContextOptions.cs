using System;
using System.Collections.Generic;
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

        private readonly bool _inMemory, _uniqueEmail;
        private readonly string _nameOrConnectionString;

        public DbContextOptions(string nameOrConnectionString,bool inMemory,bool requireUniqueEmail)
        {
            this._nameOrConnectionString = nameOrConnectionString;
            this._inMemory = inMemory;
            this._uniqueEmail = requireUniqueEmail;
        }

        public string NameOrConnectionString => _nameOrConnectionString;

        public bool InMemory => _inMemory;

        public bool RequireUniqueEmail => _uniqueEmail;
        
    }
}
