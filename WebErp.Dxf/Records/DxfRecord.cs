using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    public class DxfRecord
    {
        private readonly List<string> classhierarchy;
        internal DxfRecord()
        {
            classhierarchy = new List<string>();
        }
        public string Handle { get; set; }
        public string DimStyleHandle { get; set; }

        public List<string> Classhierarchy => classhierarchy;

        public int Flags { get; set; }
    }
}
