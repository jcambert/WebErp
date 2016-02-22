using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Web;
using WebErp.Data.Validations;
using WebErp.Models;

namespace WebErp.Validations
{
    public class UserValidation : ModelBaseValidation<ApplicationUser>
    {
        public override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            if ((entityEntry != null) && (entityEntry.State == EntityState.Added))
            {
                List<DbValidationError> source = new List<DbValidationError>();
                ApplicationUser user = entityEntry.Entity as ApplicationUser;
                if (user != null)
                {
                    if (this.Users.Any<ApplicationUser>(u => string.Equals(u.UserName, user.UserName)))
                    {
                        source.Add(new DbValidationError("User", string.Format(CultureInfo.CurrentCulture, IdentityResources.DuplicateUserName, new object[] { user.UserName })));
                    }
                    if (this.RequireUniqueEmail && this.Users.Any<ApplicationUser>(u => string.Equals(u.Email, user.Email)))
                    {
                        source.Add(new DbValidationError("User", string.Format(CultureInfo.CurrentCulture, IdentityResources.DuplicateEmail, new object[] { user.Email })));
                    }
                }
                else
                {
                    IdentityRole<string, IdentityUserRole<string>> role = entityEntry.Entity as IdentityRole<string, IdentityUserRole<string>>;
                    if ((role != null) && this.Roles.Any<IdentityRole<string, IdentityUserRole<string>>>(r => string.Equals(r.Name, role.Name)))
                    {
                        source.Add(new DbValidationError("Role", string.Format(CultureInfo.CurrentCulture, IdentityResources.RoleAlreadyExists, new object[] { role.Name })));
                    }
                }
                if (source.Any<DbValidationError>())
                {
                    return new DbEntityValidationResult(entityEntry, source);
                }
                
            }
            return new DbEntityValidationResult(entityEntry, null);
        }

        [Inject]
        public IDbSet<IdentityRole<string, IdentityUserRole<string>>> Roles { get; set; }

        [Inject]
        public IDbSet<ApplicationUser> Users { get; set; }
    }
}