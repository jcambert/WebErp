using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Parsers.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("TOLERANCE")]
    internal class DXFTolerance : DxfEntity
    {
        public string DimensionStyle { get; set; }
        private DxfPoint insertion = new DxfPoint();
        public DxfPoint InsertionPoint { get { return insertion; } }
        private DxfPoint extrusion = new DxfPoint();
        public DxfPoint ExtrusionDirection { get { return extrusion; } }
        private DxfPoint direction = new DxfPoint();
        public DxfPoint Direction { get { return direction; } }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 3:
                    DimensionStyle = value;
                    break;
                case 10:
                    InsertionPoint.X = double.Parse(value);
                    break;
                case 20:
                    InsertionPoint.Y = double.Parse(value);
                    break;
                case 30:
                    InsertionPoint.Z = double.Parse(value);
                    break;
                case 11:
                    Direction.X = double.Parse(value);
                    break;
                case 21:
                    Direction.Y = double.Parse(value);
                    break;
                case 31:
                    Direction.Z = double.Parse(value);
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
