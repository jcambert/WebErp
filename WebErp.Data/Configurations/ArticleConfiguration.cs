using System.Data.Entity;
using WebErp.Models;

namespace WebErp.Data.Configurations
{
    public class ArticleConfiguration : ModelBaseConfiguration<Article>
    {
        public ArticleConfiguration() 
        {

        }
        public override void ConfigureModel(DbModelBuilder builder)
        {
            base.ConfigureModel( builder);
            Builder.ToTable("Article");
            Builder.Property(a => a.Libelle).IsRequired().HasMaxLength(50);
            Builder.Property(a => a.Type).IsRequired();

            //Builder.HasOptional(a => a.Matiere).WithMany();
            //Builder.HasOptional(a => a.MassLinear).WithMany();
            //Builder.HasOptional(a=>a.AreaLinear)

        }
    }
}
