using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("LINE")]
    public class DxfLine : DxfEntity
    {
        private DxfPoint start = new DxfPoint();
        public DxfPoint Start { get { return start; } }

        private DxfPoint end = new DxfPoint();
        public DxfPoint End { get { return end; } }

        public double Thickness { get; set; }

        private DxfPoint extrusion = new DxfPoint();
        public DxfPoint ExtrusionDirection { get { return extrusion; } }

        internal override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 39:
                    Thickness = double.Parse(value);
                    break;
                case 10:
                    Start.X = double.Parse(value);
                    break;
                case 20:
                    Start.Y = double.Parse(value);
                    break;
                case 30:
                    Start.Z = double.Parse(value);
                    break;
                case 11:
                    End.X = double.Parse(value);
                    break;
                case 21:
                    End.Y = double.Parse(value);
                    break;
                case 31:
                    End.Z = double.Parse(value);
                    break;
                case 210:
                    ExtrusionDirection.X = double.Parse(value);
                    break;
                case 220:
                    ExtrusionDirection.Y = double.Parse(value);
                    break;
                case 230:
                    ExtrusionDirection.Z = double.Parse(value);
                    break;

            }
        }
    }
}
