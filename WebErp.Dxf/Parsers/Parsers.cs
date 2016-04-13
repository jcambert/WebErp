using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Parsers.Attributes;
namespace WebErp.Dxf.Parsers
{

    public interface ISectionParser
    {
        void Parse(int groupcode, string value);
    }

    internal abstract class SectionParser : ISectionParser
    {
        private readonly Parser _parser;
        private readonly DxfDocument _document;
       
        public SectionParser(Parser parser, DxfDocument document)
        {
            Contract.Requires(parser.IsNotNull(), "Parser Cannot be null for parsing");
            Contract.Requires(document.IsNotNull(), "Document Cannot be null for parsing");
            this._parser = parser;
            this._document = document;
            
        }

       

        public Parser Parser => _parser;

        public DxfDocument Document => _document;

       

        public virtual void Parse(int groupcode, string value)
        {

            Contract.Requires(groupcode >= 0 && groupcode <= 1071, "Group Code must be   between 0 and 1071. See http://images.autodesk.com/adsk/files/acad_dxf2.pdf");
        }
    }

    internal abstract class FieldSectionParser<TTYPE,T> : SectionParser where T:NameAttribute
    {
        private readonly Dictionary<string, PropertyInfo> _fields;
        public FieldSectionParser(Parser parser,DxfDocument doc):base(parser,doc)
        {
            this._fields = new Dictionary<string, PropertyInfo>();
            Initialize();
        }

        private void Initialize()
        {
            Type header = typeof(TTYPE);// Document.Header.GetType();
            foreach (PropertyInfo info in header.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (info.CanWrite && info.CanRead)
                {
                    object[] attrs = info.GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        T casted = attr as T;
                        if (casted != null)
                        {
                            _fields[casted.Name] = info;
                        }
                    }
                }
            }
        }
        public Dictionary<string, PropertyInfo> Fields => _fields;
    }

    internal abstract class EntitySectionParser<T>:SectionParser where T : EntityAttribute
    {
        private readonly Dictionary<string, Type> _entities = new Dictionary<string, Type>();
        public EntitySectionParser(Parser parser,DxfDocument doc):base(parser,doc)
        {
            _entities = new Dictionary<string, Type>();
            Initialize();
        }

        private void Initialize()
        {
            foreach (Type t in Assembly.GetCallingAssembly().GetTypes())
            {
                if (t.IsClass && !t.IsAbstract)
                {
                    object[] attrs = t.GetCustomAttributes(false);
                    foreach (object attr in attrs)
                    {
                        T casted = attr as T;
                        if (casted != null)
                        {
                            Entities.Add(casted.Name, t);
                        }
                    }
                }
            }
        }

        public Dictionary<string, Type> Entities => _entities;
    }

    internal abstract class DxfRecordParser : SectionParser
    {

        public DxfRecordParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {

        }

        protected DxfRecord CurrentRecord { get; set; }

        protected abstract void CreateRecord();
        #region ISectionParser Member

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 0:
                    CreateRecord();
                    break;
                case 5:
                    CurrentRecord.Handle = value;
                    break;
                case 70:
                    CurrentRecord.Flags = int.Parse(value);
                    break;
                case 105:
                    CurrentRecord.DimStyleHandle = value;
                    break;
                case 100:
                    CurrentRecord.Classhierarchy.Add(value);
                    break;
            }
        }

        #endregion
    }

    [SectionParser("HEADER")]
    internal class HeaderParser : FieldSectionParser<DxfHeader,HeaderAttribute>
    {
        PropertyInfo currentVar = null;
        public HeaderParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {

        }
        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            if (groupcode == 9)
            {
                string name = value.Trim();
                if (Fields.ContainsKey(name))
                {
                    currentVar = Fields[name];
                }
                else
                {
                    currentVar = null;
                }
            }
            else if (currentVar != null)
            {
                //at first we need to differentiate the types: nullable vs string and nullable vs rest
                if (currentVar.PropertyType.Equals(typeof(string)))
                {
                    currentVar.SetValue(Document.Header, value, null);
                }
                else if (currentVar.PropertyType.IsGenericType && currentVar.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    Type boxedType = currentVar.PropertyType.GetGenericArguments()[0];
                    if (boxedType.Equals(typeof(int)))
                    {
                        int? parsed;
                        if (value.ToLower().Contains('a') ||
                            value.ToLower().Contains('b') ||
                            value.ToLower().Contains('c') ||
                            value.ToLower().Contains('d') ||
                            value.ToLower().Contains('e') ||
                            value.ToLower().Contains('f'))
                        {
                            parsed = int.Parse(value, System.Globalization.NumberStyles.HexNumber);
                        }
                        else
                            parsed = int.Parse(value, System.Globalization.NumberStyles.Any);
                        currentVar.SetValue(Document.Header, parsed, null);
                    }
                    else if (boxedType.Equals(typeof(double)))
                    {
                        double? parsed = double.Parse(value);
                        currentVar.SetValue(Document.Header, parsed, null);
                    }
                    else if (boxedType.Equals(typeof(bool)))
                    {
                        int? parsed = int.Parse(value);
                        if (parsed != 0)
                            currentVar.SetValue(Document.Header, (bool?)true, null);
                        else
                            currentVar.SetValue(Document.Header, (bool?)false, null);
                    }
                    else if (boxedType.IsEnum)
                    {
                        object parsed = Enum.Parse(boxedType, value);
                        currentVar.SetValue(Document.Header, parsed, null);
                    }
                }
                else if (currentVar.PropertyType.Equals(typeof(DxfPoint)))
                {
                    DxfPoint p = (DxfPoint)currentVar.GetValue(Document.Header, null);
                    if (p == null)
                    {
                        p = new DxfPoint();
                        currentVar.SetValue(Document.Header, p, null);
                    }
                    switch (groupcode)
                    {
                        case 10:
                            p.X = double.Parse(value);
                            break;
                        case 20:
                            p.Y = double.Parse(value);
                            break;
                        case 30:
                            p.Z = double.Parse(value);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    [SectionParser("TABLE")]
    internal class TableParser : SectionParser
    {
        private readonly Dictionary<string, ISectionParser> tablehandlers;

        private ISectionParser currentParser = null;
        private bool waitingtableheader = false;
        private bool ignoretable = false;
        private bool firstrecordfound = false;

        public TableParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {
            tablehandlers = new Dictionary<string, ISectionParser>();
        }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            if (tablehandlers.Count == 0)
            {
                foreach (PropertyInfo info in Document.Tables.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    object[] attrs = info.GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        TableAttribute casted = attr as TableAttribute;
                        if (casted != null)
                        {
                            tablehandlers[casted.TableName] = (ISectionParser)Activator.CreateInstance(casted.TableParser);
                        }
                    }
                }
            }
            if (currentParser == null)
            {
                if (Parser.StartTable())
                {
                    waitingtableheader = true;
                }
                else if (waitingtableheader && groupcode == 2 && !ignoretable)
                {
                    if (tablehandlers.ContainsKey(value.Trim()))
                    {
                        currentParser = tablehandlers[value.Trim()];
                        waitingtableheader = false;
                        ignoretable = false;
                        firstrecordfound = false;
                    }
                    else
                    {
                        //TODO: generate warning 
                        ignoretable = true;
                    }
                }
                else if (waitingtableheader && Parser.EndTable())
                {
                    waitingtableheader = false;
                    ignoretable = false;
                }
            }
            else
            {
                if (Parser.EndTable())
                {
                    currentParser = null;
                }
                else
                {
                    if (groupcode == 0)
                    {
                        firstrecordfound = true;
                    }
                    if (firstrecordfound)
                        currentParser.Parse(groupcode, value);
                }
            }
        }

    }

    [SectionParser("CLASSES")]
    internal class ClassParser : SectionParser
    {
        public ClassParser(Parser parser,DxfDocument doc):base(parser,doc)
        {

        }

        private DxfClass currentClass;
      
        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 0:
                    currentClass = new DxfClass();
                    Document.Classes.Add(currentClass);
                    break;
                case 1:
                    currentClass.DXFRecord = value;
                    break;
                case 2:
                    currentClass.ClassName = value;
                    break;
                case 3:
                    currentClass.AppName = value;
                    break;
                case 90:
                    currentClass.ClassProxyCapabilities = (DxfClass.Caps)Enum.Parse(typeof(DxfClass.Caps), value);
                    break;
                case 280:
                    if (int.Parse(value) != 0)
                        currentClass.WasProxy = true;
                    else
                        currentClass.WasProxy = false;
                    break;
                case 281:
                    if (int.Parse(value) != 0)
                        currentClass.IsEntity = true;
                    else
                        currentClass.IsEntity = false;
                    break;
            }
        }
    }

    [SectionParser("ENTITIES")]
    internal class EntityParser : EntitySectionParser<EntityAttribute>
    {
        public EntityParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {

        }
        
        private DxfEntity currentEntity = null;
        private Stack<DxfEntity> stack = new Stack<DxfEntity>();

        public override void Parse( int groupcode, string value)
        {
           
            if (groupcode == 0)
            {
                if (value == "SEQEND")
                {
                    if (stack.Count != 0)
                        currentEntity = stack.Pop();
                }
                if (Entities.ContainsKey(value))
                {
                    if (currentEntity != null && currentEntity.HasChildren)
                    {
                        stack.Push(currentEntity);
                    }
                    currentEntity = Activator.CreateInstance(Entities[value]) as DxfEntity;
                    if (stack.Count > 0 && stack.Peek().HasChildren)
                    {
                        stack.Peek().Children.Add(currentEntity);
                    }
                    else
                    {
                        Document.Entities.Add(currentEntity);
                    }
                }
                else
                {
                    currentEntity = null;
                    //TODO: warning
                }
            }
            if (currentEntity != null)
            {
                currentEntity.Parse(groupcode, value);
            }
        }
    }

    [SectionParser("BLOCKS")]
    internal class BlockParser : SectionParser
    {
        private DxfBlock current = null;
        private bool parsingBlock = false;
        private  EntityParser entityparser ;
        private DxfDocument container;
        public static List<string> groups = new List<string>();

        public BlockParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {
            
        }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            if (groupcode == 0)
            {
                if (current == null)
                    groups.Add(value + " OUTSIDE");
                else
                    groups.Add(value);
            }
            if (current == null)
            {
                if (groupcode == 0 && value == "BLOCK")
                {
                    current = new DxfBlock();
                    container = new DxfDocument();
                    entityparser = new EntityParser(Parser, container);
                    parsingBlock = true;
                }
            }
            else
            {
                if (parsingBlock)
                {
                    if (groupcode == 0 && value == "ENDBLK")
                    {
                        current.Children.AddRange(container.Entities);
                        Document.Blocks.Add(current);
                        current = null;
                        container = null;
                    }
                    else if (groupcode == 0)
                    {
                        parsingBlock = false;
                        entityparser.Parse(groupcode, value);
                    }
                    else {
                       
                        current.Parse(groupcode, value);
                    }
                }
                else
                {
                    if (groupcode == 0 && value == "ENDBLK")
                    {
                        current.Children.AddRange(container.Entities);
                        Document.Blocks.Add(current);
                        current = null;
                        container = null;
                    }
                    else
                    {
                        entityparser.Parse( groupcode, value);
                    }
                }
            }
        }

    }

    internal class DxfAppIDParser : DxfRecordParser
    {
        public DxfAppIDParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {

        }

        #region ISectionParser Member

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 2:
                    ((DxfAppIDRecord)CurrentRecord).ApplicationName = value;
                    break;
            }
        }

        #endregion



        protected override void CreateRecord()
        {
            CurrentRecord = new DxfAppIDRecord();
            Document.Tables.AppIDs.Add(CurrentRecord as DxfAppIDRecord);
        }
    }

    internal class DxfBlockRecordParser : DxfRecordParser
    {

        public DxfBlockRecordParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {

        }


        protected override void CreateRecord()
        {
            CurrentRecord = new DxfBlockRecord();
            Document.Tables.Blocks.Add(CurrentRecord as DxfBlockRecord);
        }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            if (groupcode == 2)
            {
                ((DxfBlockRecord)CurrentRecord).BlockName = value;
            }
        }
    }

    internal class DxfDimStyleRecordParser : DxfRecordParser
    {

        public DxfDimStyleRecordParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {

        }



        protected override void CreateRecord()
        {
            CurrentRecord = new DxfDimStyleRecord();
            Document.Tables.DimStyles.Add(CurrentRecord as DxfDimStyleRecord);
        }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            if (groupcode == 2)
                ((DxfDimStyleRecord)CurrentRecord).StyleName = value;
        }
    }

    internal class DxfLayerRecordParser : DxfRecordParser
    {
        public DxfLayerRecordParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {

        }
        protected override void CreateRecord()
        {
            CurrentRecord = new DxfLayerRecord();
            Document.Tables.Layers.Add(CurrentRecord as DxfLayerRecord);
        }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 2:
                    ((DxfLayerRecord)CurrentRecord).LayerName = value;
                    break;
                case 62:
                    ((DxfLayerRecord)CurrentRecord).Color = int.Parse(value);
                    break;
                case 6:
                    ((DxfLayerRecord)CurrentRecord).LineType = value;
                    break;
            }
        }
    }

    internal class DxfLineTypeRecordParser : DxfRecordParser
    {

        private DxfLineTypeRecord.LineTypeElement _subrecord;

        public DxfLineTypeRecordParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {

        }



        protected override void CreateRecord()
        {
            CurrentRecord = new DxfLineTypeRecord();
            Document.Tables.LineTypes.Add(CurrentRecord as DxfLineTypeRecord);
        }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 2:
                    ((DxfLineTypeRecord)CurrentRecord).LineTypeName = value;
                    break;
                case 3:
                    ((DxfLineTypeRecord)CurrentRecord).Description = value;
                    break;
                case 72:
                    ((DxfLineTypeRecord)CurrentRecord).AlignmentCode = int.Parse(value);
                    break;
                case 73:
                    ((DxfLineTypeRecord)CurrentRecord).ElementCount = int.Parse(value);
                    break;
                case 40:
                    ((DxfLineTypeRecord)CurrentRecord).PatternLength = double.Parse(value);
                    break;
                case 49:
                    _subrecord = new DxfLineTypeRecord.LineTypeElement();
                    ((DxfLineTypeRecord)CurrentRecord).Elements.Add(_subrecord);
                    _subrecord.Length = double.Parse(value);
                    break;
                case 74:
                    _subrecord.Flags = (DxfLineTypeRecord.ElementFlags)Enum.Parse(typeof(DxfLineTypeRecord.ElementFlags), value);
                    break;
                case 75:
                    _subrecord.ShapeNumber = int.Parse(value);
                    break;
                case 340:
                    _subrecord.Shape = value;
                    break;
                case 46:
                    _subrecord.Scalings.Add(double.Parse(value));
                    break;
                case 50:
                    _subrecord.Rotation = double.Parse(value);
                    break;
                case 44:
                    _subrecord.XOffsets.Add(double.Parse(value));
                    break;
                case 45:
                    _subrecord.YOffsets.Add(double.Parse(value));
                    break;
                case 9:
                    _subrecord.Text = value;
                    break;
            }
        }
    }

    internal class DxfStyleRecordParser : DxfRecordParser
    {

        public DxfStyleRecordParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {

        }
        protected override void CreateRecord()
        {
            CurrentRecord = new DxfStyleRecord();
            Document.Tables.Styles.Add(CurrentRecord as DxfStyleRecord);
        }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 2:
                    ((DxfStyleRecord)CurrentRecord).StyleName = value;
                    break;
                case 40:
                    ((DxfStyleRecord)CurrentRecord).FixedHeight = double.Parse(value);
                    break;
                case 41:
                    ((DxfStyleRecord)CurrentRecord).WidthFactor = double.Parse(value);
                    break;
                case 50:
                    ((DxfStyleRecord)CurrentRecord).ObliqueAngle = double.Parse(value);
                    break;
                case 71:
                    ((DxfStyleRecord)CurrentRecord).GenerationFlags = (DxfStyleRecord.TextGenerationFlags)Enum.Parse(typeof(DxfStyleRecord.TextGenerationFlags), value);
                    break;
                case 42:
                    ((DxfStyleRecord)CurrentRecord).LastUsedHeight = double.Parse(value);
                    break;
                case 3:
                    ((DxfStyleRecord)CurrentRecord).FontFileName = value;
                    break;
                case 4:
                    ((DxfStyleRecord)CurrentRecord).BigFontFileName = value;
                    break;
            }
        }
    }

    internal class DxfUCSRecordParser : DxfRecordParser
    {
        public DxfUCSRecordParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {

        }

        protected override void CreateRecord()
        {
            CurrentRecord = new DxfUCSRecord();
            Document.Tables.UCS.Add(CurrentRecord as DxfUCSRecord);
        }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 2:
                    ((DxfUCSRecord)CurrentRecord).UCSName = value;
                    break;
                case 10:
                    ((DxfUCSRecord)CurrentRecord).Origin.X = double.Parse(value);
                    break;
                case 20:
                    ((DxfUCSRecord)CurrentRecord).Origin.Y = double.Parse(value);
                    break;
                case 30:
                    ((DxfUCSRecord)CurrentRecord).Origin.Z = double.Parse(value);
                    break;
                case 11:
                    ((DxfUCSRecord)CurrentRecord).XAxis.X = double.Parse(value);
                    break;
                case 21:
                    ((DxfUCSRecord)CurrentRecord).XAxis.Y = double.Parse(value);
                    break;
                case 31:
                    ((DxfUCSRecord)CurrentRecord).XAxis.Z = double.Parse(value);
                    break;
                case 12:
                    ((DxfUCSRecord)CurrentRecord).YAxis.X = double.Parse(value);
                    break;
                case 22:
                    ((DxfUCSRecord)CurrentRecord).YAxis.Y = double.Parse(value);
                    break;
                case 32:
                    ((DxfUCSRecord)CurrentRecord).YAxis.Z = double.Parse(value);
                    break;
            }
        }
    }

    internal class DxfViewRecordParser : DxfRecordParser
    {
        public DxfViewRecordParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {

        }

        protected override void CreateRecord()
        {
            CurrentRecord = new DxfViewRecord();
            Document.Tables.Views.Add(CurrentRecord as DxfViewRecord);
        }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 2:
                    ((DxfViewRecord)CurrentRecord).ViewPortName = value;
                    break;
                case 40:
                    ((DxfViewRecord)CurrentRecord).Height = double.Parse(value);
                    break;
                case 41:
                    ((DxfViewRecord)CurrentRecord).Width = double.Parse(value);
                    break;
                case 10:
                    ((DxfViewRecord)CurrentRecord).Center.X = double.Parse(value);
                    break;
                case 20:
                    ((DxfViewRecord)CurrentRecord).Center.Y = double.Parse(value);
                    break;
                case 11:
                    ((DxfViewRecord)CurrentRecord).Direction.X = double.Parse(value);
                    break;
                case 21:
                    ((DxfViewRecord)CurrentRecord).Direction.Y = double.Parse(value);
                    break;
                case 31:
                    ((DxfViewRecord)CurrentRecord).Direction.Z = double.Parse(value);
                    break;
                case 12:
                    ((DxfViewRecord)CurrentRecord).Target.X = double.Parse(value);
                    break;
                case 22:
                    ((DxfViewRecord)CurrentRecord).Target.Y = double.Parse(value);
                    break;
                case 32:
                    ((DxfViewRecord)CurrentRecord).Target.Z = double.Parse(value);
                    break;
                case 42:
                    ((DxfViewRecord)CurrentRecord).LensLength = double.Parse(value);
                    break;
                case 43:
                    ((DxfViewRecord)CurrentRecord).FrontClippingPlane = double.Parse(value);
                    break;
                case 44:
                    ((DxfViewRecord)CurrentRecord).BackClippingPlane = double.Parse(value);
                    break;
                case 50:
                    ((DxfViewRecord)CurrentRecord).TwistAngle = double.Parse(value);
                    break;
                case 71:
                    ((DxfViewRecord)CurrentRecord).ViewMode = int.Parse(value);
                    break;
            }
        }
    }

    internal class DxfVPortRecordParser : DxfRecordParser
    {
        public DxfVPortRecordParser(Parser parser, DxfDocument doc) : base(parser, doc)
        {

        }

        protected override void CreateRecord()
        {
            CurrentRecord = new DxfVPortRecord();
            Document.Tables.VPorts.Add(CurrentRecord as DxfVPortRecord);
        }

        public override void Parse(int groupcode, string value)
        {
            base.Parse(groupcode, value);
            switch (groupcode)
            {
                case 2:
                    ((DxfVPortRecord)CurrentRecord).VPortName = value;
                    break;
                case 10:
                    ((DxfVPortRecord)CurrentRecord).LowerLeftCorner.X = double.Parse(value);
                    break;
                case 20:
                    ((DxfVPortRecord)CurrentRecord).LowerLeftCorner.Y = double.Parse(value);
                    break;
                case 11:
                    ((DxfVPortRecord)CurrentRecord).UpperRightCorner.X = double.Parse(value);
                    break;
                case 21:
                    ((DxfVPortRecord)CurrentRecord).UpperRightCorner.Y = double.Parse(value);
                    break;
                case 12:
                    ((DxfVPortRecord)CurrentRecord).Center.X = double.Parse(value);
                    break;
                case 22:
                    ((DxfVPortRecord)CurrentRecord).Center.Y = double.Parse(value);
                    break;
                case 13:
                    ((DxfVPortRecord)CurrentRecord).SnapBase.X = double.Parse(value);
                    break;
                case 23:
                    ((DxfVPortRecord)CurrentRecord).SnapBase.Y = double.Parse(value);
                    break;
                case 14:
                    ((DxfVPortRecord)CurrentRecord).SnapSpacing.X = double.Parse(value);
                    break;
                case 24:
                    ((DxfVPortRecord)CurrentRecord).SnapSpacing.Y = double.Parse(value);
                    break;
                case 15:
                    ((DxfVPortRecord)CurrentRecord).GridSpacing.X = double.Parse(value);
                    break;
                case 25:
                    ((DxfVPortRecord)CurrentRecord).GridSpacing.Y = double.Parse(value);
                    break;
                case 16:
                    ((DxfVPortRecord)CurrentRecord).Direction.X = double.Parse(value);
                    break;
                case 26:
                    ((DxfVPortRecord)CurrentRecord).Direction.Y = double.Parse(value);
                    break;
                case 36:
                    ((DxfVPortRecord)CurrentRecord).Direction.Z = double.Parse(value);
                    break;
                case 17:
                    ((DxfVPortRecord)CurrentRecord).Target.X = double.Parse(value);
                    break;
                case 27:
                    ((DxfVPortRecord)CurrentRecord).Target.Y = double.Parse(value);
                    break;
                case 37:
                    ((DxfVPortRecord)CurrentRecord).Target.Z = double.Parse(value);
                    break;
                case 40:
                    ((DxfVPortRecord)CurrentRecord).Height = double.Parse(value);
                    break;
                case 41:
                    ((DxfVPortRecord)CurrentRecord).AspectRatio = double.Parse(value);
                    break;
                case 42:
                    ((DxfVPortRecord)CurrentRecord).LensLength = double.Parse(value);
                    break;
                case 43:
                    ((DxfVPortRecord)CurrentRecord).FrontClippingPlane = double.Parse(value);
                    break;
                case 44:
                    ((DxfVPortRecord)CurrentRecord).BackClippingPlane = double.Parse(value);
                    break;
                case 50:
                    ((DxfVPortRecord)CurrentRecord).SnapRotationAngle = double.Parse(value);
                    break;
                case 51:
                    ((DxfVPortRecord)CurrentRecord).TwistAngle = double.Parse(value);
                    break;
                case 71:
                    ((DxfVPortRecord)CurrentRecord).ViewMode = int.Parse(value);
                    break;
                case 72:
                    ((DxfVPortRecord)CurrentRecord).CircleZoomPercent = int.Parse(value);
                    break;
                case 73:
                    ((DxfVPortRecord)CurrentRecord).FastZoomSetting = int.Parse(value);
                    break;
                case 74:
                    ((DxfVPortRecord)CurrentRecord).UCSICONSetting = int.Parse(value);
                    break;
                case 75:
                    ((DxfVPortRecord)CurrentRecord).SnapEnabled = int.Parse(value);
                    break;
                case 76:
                    ((DxfVPortRecord)CurrentRecord).GridEnabled = int.Parse(value);
                    break;
                case 77:
                    ((DxfVPortRecord)CurrentRecord).SnapStyle = int.Parse(value);
                    break;
                case 78:
                    ((DxfVPortRecord)CurrentRecord).SnapIsoPair = int.Parse(value);
                    break;
            }
        }
    }
}
