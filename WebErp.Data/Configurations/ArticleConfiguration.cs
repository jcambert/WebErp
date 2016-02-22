
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Models;

namespace WebErp.Data.Configurations
{
    public class ArticleConfiguration : ModelBaseConfiguration<Article>
    {
        public ArticleConfiguration(DbModelBuilder builder) : base(builder)
        {

        }
        public override void ConfigureModel()
        {
            Builder.ToTable("Article");
            Builder.Property(a => a.Libelle).IsRequired().HasMaxLength(50);
            Builder.Property(a => a.Type).IsRequired();
        }
    }
}
