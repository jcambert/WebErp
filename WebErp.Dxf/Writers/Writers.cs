using System;
using System.Collections.Generic;
#if DEBUG
using System.Diagnostics;
#endif
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
            
            await WriteSection(WriteHeader);
            await WriteSection(WriteClasses);
            await WriteSection(WriteTables);
            await WriteSection(WriteEntities);
            await writer.WriteLineAsync(DxfDocument.END_DOCUMENT.ToString());
            await writer.WriteAsync(DxfDocument.END);

        }

        private async Task WriteSection(Func<Task> section)
        {
            await writer.WriteLineAsync(DxfDocument.START_GROUP_CODE.ToString());
            await writer.WriteLineAsync(DxfDocument.SECTION);
            await writer.WriteLineAsync(DxfDocument.START_SECTION.ToString());
            
            await section();
            await writer.WriteLineAsync(DxfDocument.END_SECTION.ToString());
            await writer.WriteLineAsync(DxfDocument.ENDSECTION);
            
        }

        private async Task WriteHeader()
        {
            await writer.WriteLineAsync(DxfDocument.HEADER);
            var fields = FieldSectionProvider.GetFields<DxfHeader, HeaderAttribute>();
            foreach (var field in fields)
            {
                if (field.Value.PropertyType.Equals(typeof(string)))
                {
                    try {
                        var value = field.Value.GetValue(document.Header)?.ToString();
                        if (value == null) continue;
                        var key = field.Key;
                        await writer.WriteLineAsync("9");
                        await writer.WriteLineAsync(key);
                        await writer.WriteLineAsync("1");
                        await writer.WriteLineAsync(value);
                    }catch(Exception ex)
                    {
#if DEBUG
                        Debugger.Break();
#endif
                    }
                }
            }
        }

        private async Task WriteClasses()
        {
            await writer.WriteLineAsync(DxfDocument.CLASSES);
        }

        private async Task WriteTables()
        {
            await writer.WriteLineAsync(DxfDocument.TABLES);
        }

        private async Task WriteEntities()
        {
            await writer.WriteLineAsync(DxfDocument.ENTITIES);
            foreach (var entity in Document.Entities)
            {
                var s = entity.ToString();
                if (s.IsNullOrEmpty()) continue;
                await writer.WriteLineAsync(s);
            }
        }

    }

    
}
