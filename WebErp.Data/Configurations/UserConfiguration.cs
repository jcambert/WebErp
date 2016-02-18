using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Models;

namespace WebErp.Data.Configurations
{
    public class UserConfiguration : ModelBaseConfiguration<User>
    {
        public UserConfiguration(ModelBuilder builder):base(builder)
        {

        }
        protected override void ConfigureModel()
        {
            Builder.Property(u => u.Username).IsRequired().HasMaxLength(100);
            Builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
            Builder.Property(u => u.HashedPassword).IsRequired().HasMaxLength(200);
            Builder.Property(u => u.Salt).IsRequired().HasMaxLength(200);
            Builder.Property(u => u.IsLocked).IsRequired();
            Builder.Property(u => u.DateCreated);
        }
    }
}
