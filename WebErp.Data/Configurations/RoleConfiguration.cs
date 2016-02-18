using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Models;

namespace WebErp.Data.Configurations
{
    public class RoleConfiguration: ModelBaseConfiguration<Role>
    {
        public RoleConfiguration(ModelBuilder builder):base(builder)
        {

        }

        protected override void ConfigureModel()
        {
            Builder.Property(ur => ur.Code).IsRequired().HasMaxLength(50);
        }
    }
}
