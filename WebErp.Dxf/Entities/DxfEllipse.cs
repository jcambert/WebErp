using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("ELLIPSE")]
    public class DxfEllipse : DxfEntity
    {
        private DxfPoint center = new DxfPoint();
        public DxfPoint Center => center;

        private DxfPoint mainaxis = new DxfPoint();
        public DxfPoint MainAxis => mainaxis;

        private DxfPoint extrusion = new DxfPoint();
        public DxfPoint ExtrusionDirection => extrusion;

        public double AxisRatio { get; set; }

        public double StartParam { get; set; }
        public double EndParam { get; set; }

        internal override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 10:
                    Center.X = double.Parse(value);
                    break;
                case 20:
                    Center.Y = double.Parse(value);
                    break;
                case 30:
                    Center.Z = double.Parse(value);
                    break;
                case 11:
                    MainAxis.X = double.Parse(value);
                    break;
                case 21:
                    MainAxis.Y = double.Parse(value);
                    break;
                case 31:
                    MainAxis.Z = double.Parse(value);
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
                case 40:
                    AxisRatio = double.Parse(value);
                    break;
                case 41:
                    StartParam = double.Parse(value);
                    break;
                case 42:
                    EndParam = double.Parse(value);
                    break;
            }
        }
    }
}
