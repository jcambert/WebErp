using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Parsers.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("POINT")]
    internal class DxfPointEntity : DxfEntity
    {
        private DxfPoint location = new DxfPoint();
        public DxfPoint Location { get { return location; } }

        public double Thickness { get; set; }

        private DxfPoint extrusion = new DxfPoint();
        public DxfPoint ExtrusionDirection { get { return extrusion; } }

        public double XAxisAngle { get; set; }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 10:
                    Location.X = double.Parse(value);
                    break;
                case 20:
                    Location.Y = double.Parse(value);
                    break;
                case 30:
                    Location.Z = double.Parse(value);
                    break;
                case 39:
                    Thickness = double.Parse(value);
                    break;
                case 50:
                    XAxisAngle = double.Parse(value);
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
