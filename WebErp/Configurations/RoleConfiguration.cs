

using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq.Expressions;
using WebErp.Data.Configurations;

namespace WebErp.Configurations
{
    public class RoleConfiguration : ModelBaseConfiguration<IdentityRole<string, IdentityUserRole<string>>>
    {
        public RoleConfiguration(DbModelBuilder builder) : base(builder)
        {

        }

        public override void ConfigureModel()
        {
            Builder.ToTable("Roles");
            IndexAttribute attribute2 = new IndexAttribute("RoleNameIndex")
            {
                IsUnique = true
            };
            Builder.Property(r => r.Name).IsRequired().HasMaxLength(0x100).HasColumnAnnotation("Index", new IndexAnnotation(attribute2));
            Builder.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
        }
    }
}