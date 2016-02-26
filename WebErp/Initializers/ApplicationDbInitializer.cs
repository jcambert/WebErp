using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebErp.Models;

namespace WebErp.Initializers
{
    public class ApplicationDbInitializer: DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
           
            context.ArticleSet.Add(new Article() { Societe = 999, Code = "123456" });
            context.Commit();
            base.Seed(context);
        }
    }
}