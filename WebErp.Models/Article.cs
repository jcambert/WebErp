using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Models
{
    public class Article:ModelBase
    {
        

        /// <summary>
        /// Libelle Article
        /// </summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Type Article
        /// </summary>
        public TypeArticle Type { get; set; }
    }
}
