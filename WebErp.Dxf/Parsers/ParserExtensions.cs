using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Parsers.Attributes;

namespace WebErp.Dxf.Parsers
{
    internal static class ParserExtensions
    {
        private static bool isWellFormed(this Parser parser)
        {
            return !(parser.GroupCode.IsNull() || parser.Value.IsNullOrEmpty());
        }

        public static bool StartSection(this Parser parser)
        {
            if (!parser.isWellFormed()) return false;
            return parser.GroupCode == DxfDocument.START_GROUP_CODE && parser.Value.Trim() == DxfDocument.SECTION;
        }

        public static bool EndSection(this Parser parser)
        {
            if (!parser.isWellFormed()) return false;
            return parser.GroupCode == DxfDocument.START_GROUP_CODE && parser.Value.Trim() == DxfDocument.ENDSECTION;
        }


        public static bool StartTable(this Parser parser)
        {
            if (!parser.isWellFormed()) return false;
            return parser.GroupCode == DxfDocument.START_GROUP_CODE && parser.Value.Trim() == DxfDocument.TABLE;
        }

        public static bool EndTable(this Parser parser)
        {
            if (!parser.isWellFormed()) return false;
            return parser.GroupCode == DxfDocument.START_GROUP_CODE && parser.Value.Trim() == DxfDocument.ENDTABLE;
        }

        public static Dictionary<string, SectionParser> GetParsers(this Parser parser,DxfDocument document)
        {
            Dictionary<string, SectionParser> result = new Dictionary<string, SectionParser>();

            var assembly = Assembly.GetAssembly(typeof(DxfDocument));
            foreach (var item in assembly.GetTypesWithAttribute<SectionParserAttribute>())
            {
                result[item.GetCustomAttribute<SectionParserAttribute>().SectionName] = Activator.CreateInstance( item,new object[] {parser, document}) as SectionParser;
            }
            return result;
        }

      
    }
}
