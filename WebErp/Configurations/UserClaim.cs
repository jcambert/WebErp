using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebErp.Data.Configurations;

namespace WebErp.Configurations
{
    
    public class UserClaim : ModelBaseConfiguration<IdentityUserClaim<string>>
    {
        public UserClaim(DbModelBuilder builder) : base(builder)
        {

        }

        public override void ConfigureModel()
        {
            Builder.ToTable("UserClaims");
        }
    }
}