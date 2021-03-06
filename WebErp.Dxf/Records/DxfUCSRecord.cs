﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    public class DxfUCSRecord : DxfRecord
    {
        private readonly DxfPoint origin;
        private readonly DxfPoint xaxis;
        private readonly DxfPoint yaxis;

        internal DxfUCSRecord():base()
        {
            origin = new DxfPoint();
            xaxis = new DxfPoint();
            yaxis = new DxfPoint();
        }
        public string UCSName { get; set; }

        public DxfPoint Origin => origin;

        public DxfPoint XAxis => xaxis;
         
        public DxfPoint YAxis => yaxis;
    }
}
