﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf
{
    internal static class TextReaderExtensions
    {
        public static void ReadDXFEntry(this TextReader obj, out int? groupcode, out string value)
        {
            string line = obj.ReadLine();
            if (line == null)
            {
                groupcode = null;
                value = null;
                return;
            }
            int buf;
            if (int.TryParse(line.Trim(), out buf))
            {
                groupcode = buf;
                value = obj.ReadLine();
            }
            else
            {
                value = null;
                groupcode = null;
            }

        }
    }
}

