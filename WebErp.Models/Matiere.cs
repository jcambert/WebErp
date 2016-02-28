using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace WebErp.Models
{
    public class Matiere:ModelBase
    {
        public Density Desnite { get; set; }

        public string Numero { get; set; }

        public string Symbole { get; set; }

        public string  CodeNfa{ get; set; }

        public string CodeUns { get; set; }

        public string CodeAstm { get; set; }

        public string  CodeAisi { get; set; }

    }
}
