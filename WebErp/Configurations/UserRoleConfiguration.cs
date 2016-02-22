using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebErp.Data.Configurations;

namespace WebErp.Configurations
{
    public class UserRoleConfiguration : ModelBaseConfiguration<IdentityUserRole<string>>
    {
        public UserRoleConfiguration(DbModelBuilder builder) : base(builder)
        {

        }

        public override void ConfigureModel()
        {
            Builder.HasKey(r => new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("UserRoles");
        }
    }
}