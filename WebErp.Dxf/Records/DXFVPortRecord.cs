using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    public class DxfVPortRecord : DxfRecord
    {
        private readonly DxfPoint lowerleft;
        private readonly DxfPoint upperright;
        private readonly DxfPoint center;
        private readonly DxfPoint snapbase;
        private readonly DxfPoint snapspacing;
        private readonly DxfPoint gridspacing;
        private readonly DxfPoint direction;
        private readonly DxfPoint target;
        internal DxfVPortRecord():base()
        {
            lowerleft = new DxfPoint();
            upperright = new DxfPoint();
            center = new DxfPoint();
            snapbase = new DxfPoint();
            snapspacing = new DxfPoint();
            gridspacing = new DxfPoint();
            direction = new DxfPoint();
            target = new DxfPoint();
        }
        public string VPortName { get; set; }

        public DxfPoint LowerLeftCorner => lowerleft;

        public DxfPoint UpperRightCorner => upperright;

        public DxfPoint Center => center;

        public DxfPoint SnapBase => snapbase;

        public DxfPoint SnapSpacing => snapspacing;

        public DxfPoint GridSpacing => gridspacing;

        public DxfPoint Direction => direction;

        public DxfPoint Target => target;

        public double Height { get; set; }
        public double AspectRatio { get; set; }
        public double LensLength { get; set; }
        public double FrontClippingPlane { get; set; }
        public double BackClippingPlane { get; set; }
        public double SnapRotationAngle { get; set; }
        public double TwistAngle { get; set; }
        public int ViewMode { get; set; }
        public int CircleZoomPercent { get; set; }
        public int FastZoomSetting { get; set; }
        public int UCSICONSetting { get; set; }
        public int SnapEnabled { get; set; }
        public int GridEnabled { get; set; }
        public int SnapStyle { get; set; }
        public int SnapIsoPair { get; set; }

    }
}
