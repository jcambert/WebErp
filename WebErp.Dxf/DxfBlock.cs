using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    public class DxfBlock : DxfEntity
    {
        private readonly DxfPoint basePoint ;
        internal DxfBlock():base()
        {
            basePoint = new DxfPoint();
        }
        public string BlockName { get; set; }
        public int BlockFlags { get; set; }

        internal DxfPoint BasePoint => basePoint;

        public string XRef { get; set; }

        internal override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 2:
                case 3:
                    BlockName = value;
                    break;
                case 70:
                    BlockFlags = int.Parse(value);
                    break;
                case 1:
                    XRef = value;
                    break;
                case 10:
                    BasePoint.X = double.Parse(value);
                    break;
                case 20:
                    BasePoint.Y = double.Parse(value);
                    break;
                case 30:
                    BasePoint.Z = double.Parse(value);
                    break;
            }
        }

    }
}
