using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebErp.Data.Configurations;

namespace WebErp.Configurations
{
   
    public class UserLoginConfiguration : ModelBaseConfiguration<IdentityUserLogin<string>>
    {
        public UserLoginConfiguration(DbModelBuilder builder) : base(builder)
        {

        }

        public override void ConfigureModel()
        {
            Builder.HasKey(l => new { LoginProvider = l.LoginProvider, ProviderKey = l.ProviderKey, UserId = l.UserId }).ToTable("UserLogins");
        }
    }
}