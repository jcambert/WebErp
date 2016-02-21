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

        private readonly bool _inMemory;
        private readonly string _nameOrConnectionString;

        public DbContextOptions(string nameOrConnectionString,bool inMemory)
        {
            this._nameOrConnectionString = nameOrConnectionString;
            this._inMemory = inMemory;
        }

        public string NameOrConnectionString => _nameOrConnectionString;

        public bool InMemory => _inMemory;
    }
}
