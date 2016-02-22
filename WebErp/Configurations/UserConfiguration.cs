using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebErp.Data.Configurations;
using WebErp.Models;

namespace WebErp.Configurations
{
    public class UserConfiguration : ModelBaseConfiguration<ApplicationUser>
    {
        public UserConfiguration(DbModelBuilder builder) : base(builder)
        {

        }

        protected override void ConfigureModel()
        {
            Builder.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            Builder.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            Builder.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            IndexAttribute indexAttribute = new IndexAttribute("UserNameIndex")
            {
                IsUnique = true
            };
            Builder.Property(u => u.UserName).IsRequired().HasMaxLength(0x100).HasColumnAnnotation("Index", new IndexAnnotation(indexAttribute));
            configuration.Property((Expression<Func<TUser, string>>)(u => u.Email)).HasMaxLength(0x100);
        }
    }
}