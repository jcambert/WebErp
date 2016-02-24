using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebErp.Data.Configurations;

namespace WebErp.Configurations
{
    
    public class UserClaimConfiguration : ModelBaseConfiguration<IdentityUserClaim<string>>
    {
        public UserClaimConfiguration()
        {

        }

        public override void ConfigureModel(DbModelBuilder builder)
        {
            base.ConfigureModel(builder);
            Builder.ToTable("UserClaims");
        }
    }
}