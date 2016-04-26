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
        private readonly DxfPoint start ;
        private readonly DxfPoint end ;
        private readonly DxfPoint extrusion ;
        public DxfLine()
        {
            start = new DxfPoint();
            end = new DxfPoint();
            extrusion = new DxfPoint();
        }

        [EntityCode(10, "X")]
        [EntityCode(20, "X")]
        [EntityCode(30, "X")]
        public DxfPoint Start => start;
        [EntityCode(11, "X")]
        [EntityCode(21, "X")]
        [EntityCode(31, "X")]
        public DxfPoint End => end;
        [EntityCode(39)]
        public double Thickness { get; set; }
        [EntityCode(210, "X")]
        [EntityCode(220, "Y")]
        [EntityCode(230, "Z")]
        public DxfPoint ExtrusionDirection => extrusion;

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

        /*public override string ToString()
        {
            return base.ToString();

        }*/
    }
}
