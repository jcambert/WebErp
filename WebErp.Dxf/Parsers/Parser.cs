using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf.Parsers
{
    internal class Parser
    {
        readonly Stream _file;

        LoadState state ;
        ISectionParser currentParser ;
        int? groupCode;
        string value;
        readonly Dictionary<string, SectionParser> parsers;
        readonly DxfDocument doc ;
        public Parser(Stream file)
        {
            this._file = file;
            state = LoadState.OutsideSection;
            currentParser = null;
            groupCode = null;
            doc = new DxfDocument();
            parsers = this.GetParsers(doc);
        }

        public int? GroupCode => groupCode;

        public string Value => value;

        public DxfDocument Parse()
        {

            

            TextReader reader = new StreamReader(_file);

            reader.ReadDXFEntry(out groupCode, out value);

            while (groupCode != null)
            {
                switch (state)
                {
                    case LoadState.OutsideSection:
                        if (this.StartSection())
                        {
                            state = LoadState.InSection;
                            reader.ReadDXFEntry(out groupCode, out value);
                            if (groupCode != 2)
                                throw new Exception("Error in DXF Format. Must have 2 code after 0/SECTION");
                            value = value.Trim();
                            if (parsers.ContainsKey(value))
                                currentParser = parsers[value];
                        }
                        break;
                    case LoadState.InSection:
                        if (this.EndSection())
                        {
                            state = LoadState.OutsideSection;
                            
                        }
                        else
                        {
                            if (currentParser != null)
                            {
                                currentParser.Parse( (int)groupCode, value);
                            }
                        }
                        break;
                    default:
                        break;
                }
                reader.ReadDXFEntry(out groupCode, out value);
            }

            return doc;
        }
    }

    internal enum LoadState
    {
        OutsideSection,
        InSection
    }
}
