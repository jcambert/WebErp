using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;

namespace WebErp.Dxf.Entities
{
    [Entity("INSERT")]
    public class DxfInsert : DxfEntity
    {
        public string BlockName { get; set; }
        private DxfPoint insertionPoint = new DxfPoint();
        public DxfPoint InsertionPoint { get { return insertionPoint; } }
        private DxfPoint scaling = new DxfPoint();
        public DxfPoint Scaling { get { return scaling; } }
        public double? RotationAngle { get; set; }
        private DxfPoint extrusionDirection = new DxfPoint();
        public DxfPoint ExtrusionDirection { get { return extrusionDirection; } }

        internal override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 2:
                    BlockName = value;
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
                case 41:
                    Scaling.X = double.Parse(value);
                    break;
                case 42:
                    Scaling.Y = double.Parse(value);
                    break;
                case 43:
                    Scaling.Z = double.Parse(value);
                    break;
            }
        }
    }
}
