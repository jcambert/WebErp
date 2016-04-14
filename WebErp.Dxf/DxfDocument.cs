using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Parsers;

namespace WebErp.Dxf
{
    public sealed class DxfDocument
    {
        //public const string START_GROUP = "0";
        public const string SECTION = "SECTION";
        public const string ENDSECTION = "ENDSEC";
        public const string TABLE = "TABLE";
        public const string ENDTABLE = "ENDTAB";
        public const string END = "EOF";

        public const int START_GROUP_CODE = 0;


        private readonly DxfHeader _header;
        private readonly DxfTables _tables ;
        private readonly List<DxfClass> _classes ;
        private readonly List<DxfBlock> _blocks;
        private readonly List<DxfEntity> _entities;
        
        public DxfDocument()
        {
            _header = new DxfHeader();
            _tables = new DxfTables();
            _classes = new List<DxfClass>();
            _blocks = new List<DxfBlock>();
            _entities = new List<DxfEntity>();
        }

        public DxfHeader Header => _header;   

        public DxfTables Tables => _tables;

        public List<DxfClass> Classes => _classes;

        public List<DxfBlock> Blocks => _blocks;

        public List<DxfEntity> Entities => _entities;

        public string Filename { get; internal set; }

        public static DxfDocument Load(string filename)
        {
            Contract.Requires(!filename.IsNullOrEmpty(), "File name cannot be null nor empty");
            FileStream stream = new FileStream(filename,FileMode.Open);
            
            var doc= Load(stream);
            doc.Filename = filename;
            stream.Close();
            return doc;
        }

        internal static DxfDocument Load(Stream file)
        {
            Contract.Requires(file.IsNotNull(), "File as stream cannot be null");

            return new Parser(file).Parse();

            
        }




    }

    

}
