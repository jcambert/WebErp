using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Models.eTole;

namespace WebErp.Data.Configurations
{
    public class PartItemConfiguration: ModelConfiguration<PartItem>
    {
        
        protected override void ConfigureKey()
        {
            Builder.HasKey(e => e.Id);
            Builder.Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class PartPropertyConfiguration : ModelConfiguration<PartProperty>
    {

        protected override void ConfigureKey()
        {
            Builder.HasKey(e => e.Id);
            Builder.Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
