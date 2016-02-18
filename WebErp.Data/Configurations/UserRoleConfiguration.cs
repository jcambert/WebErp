﻿using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Models;

namespace WebErp.Data.Configurations
{
    public class UserRoleConfiguration: ModelBaseConfiguration<UserRole>
    {
        public UserRoleConfiguration(ModelBuilder builder):base(builder)
        {

        }

        protected override void ConfigureModel()
        {
            Builder.Property(ur => ur.UserId).IsRequired();
            Builder.Property(ur => ur.RoleId).IsRequired();
        }
    }
}
