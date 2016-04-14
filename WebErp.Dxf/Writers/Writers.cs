using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;

namespace WebErp.Dxf.Writers
{

    public interface ISectionWriter
    {
        void Write();
    }

    public class Writer
    {

        private readonly DxfDocument document;
        private readonly StreamWriter writer;
        internal Writer(DxfDocument doc, StreamWriter writer)
        {
            Contract.Requires(doc.IsNotNull(), "Dxf Document cannot be null for writing");
            Contract.Requires(writer.IsNotNull(), "Writer cannot be null for writing");
            this.document = doc;
            this.writer = writer;
        }
        public DxfDocument Document => document;

        public async Task Write()
        {
            await WriteSection(WriteHead);
            
        }

        private async Task WriteSection(Func<Task> writeHead)
        {
            await writer.WriteLineAsync(DxfDocument.START_GROUP_CODE.ToString());
            await writer.WriteLineAsync(DxfDocument.SECTION);
            await WriteHead();
            await writer.WriteLineAsync(DxfDocument.START_GROUP_CODE.ToString());
            await writer.WriteLineAsync(DxfDocument.END);
            
        }

        private async Task WriteHead()
        {
            var fields = FieldSectionProvider.GetFields<DxfHeader, HeaderAttribute>();
            foreach (var field in fields)
            {
                if (field.Value.PropertyType.Equals(typeof(string)))
                {
                    var value = field.Value.GetValue(document.Header).ToString();
                    if (value == null) continue;
                    var key = field.Key;
                    await writer.WriteLineAsync("9");
                    await writer.WriteLineAsync(key);
                    await writer.WriteLineAsync("1");
                    await writer.WriteLineAsync(value);
                }
            }
        }

        private async Task WriteEnd(TextWriter writer)
        {

        }
    }

    public class HeaderWriter : ISectionWriter
    {
        public void Write()
        {
            throw new NotImplementedException();
        }
    }
}
