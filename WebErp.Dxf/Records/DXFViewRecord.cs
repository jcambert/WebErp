using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    internal class DxfViewRecord : DxfRecord
    {
        private readonly DxfPoint center ;
        private readonly DxfPoint direction;
        private readonly DxfPoint target ;

        public DxfViewRecord()
        {
            center = new DxfPoint();
            direction = new DxfPoint();
            target = new DxfPoint();
        }
        public string ViewPortName { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }

        public DxfPoint Center => center;

        public DxfPoint Direction => direction;
        
        public DxfPoint Target => target;

        public double FrontClippingPlane { get; set; }
        public double BackClippingPlane { get; set; }
        public double TwistAngle { get; set; }
        public double LensLength { get; set; }
        public int ViewMode { get; set; }
    }
}

