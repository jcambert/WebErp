
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Models;

namespace WebErp.Data.Configurations
{
    public class RoleConfiguration: ModelBaseConfiguration<Role>
    {
        public RoleConfiguration(DbModelBuilder builder):base(builder)
        {

        }

        protected override void ConfigureModel()
        {
            Builder.Property(ur => ur.Code).IsRequired().HasMaxLength(50);
        }
    }
}
