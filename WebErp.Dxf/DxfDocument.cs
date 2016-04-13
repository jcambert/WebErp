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
        public const string SECTION = "SECTION";
        public const string ENDSECTION = "ENDSEC";
        public const string TABLE = "TABLE";
        public const string ENDTABLE = "ENDTAB";


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

        internal DxfHeader Header => _header;   

        internal DxfTables Tables => _tables;

        internal List<DxfClass> Classes => _classes;

        internal List<DxfBlock> Blocks => _blocks;

        internal List<DxfEntity> Entities => _entities;

        public static DxfDocument Load(string filename)
        {
            Contract.Requires(!filename.IsNullOrEmpty(), "File name cannot be null nor empty");
            FileStream stream = new FileStream(filename,FileMode.Open);
            return Load(stream);
        }

        public static DxfDocument Load(Stream file)
        {
            Contract.Requires(file.IsNotNull(), "File as stream cannot be null");

            return new Parser(file).Parse();

            
        }




    }

    

}
