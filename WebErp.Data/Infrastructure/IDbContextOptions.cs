namespace WebErp.Data.Infrastructure
{
    public interface IDbContextOptions
    {
        string NameOrConnectionString
        {
            get;
        }

        bool InMemory
        {
            get;
        }
        bool RequireUniqueEmail { get;}
        bool RecreateDatabase { get;  }
    }
}