using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    public class DxfStyleRecord : DxfRecord
    {
        internal DxfStyleRecord():base()
        {

        }
        public string StyleName { get; set; }
        public double FixedHeight { get; set; }
        public double WidthFactor { get; set; }
        public double ObliqueAngle { get; set; }
        [Flags]
        public enum TextGenerationFlags
        {
            MirrorX = 2,
            MirrorY = 4
        }

        public TextGenerationFlags GenerationFlags { get; set; }

        public double LastUsedHeight { get; set; }
        public string FontFileName { get; set; }
        public string BigFontFileName { get; set; }
    }
}
