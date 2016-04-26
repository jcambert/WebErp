using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp;
namespace WebErp.Dxf
{
    public static class Extensions
    {
        public static async Task Save(this DxfDocument doc)
        {
            if (doc.Filename.IsNullOrEmpty())
                throw new ApplicationException("Filename is empty!");
            await doc.SaveAs(doc.Filename);
        }

        public static async Task SaveAs(this DxfDocument doc,string filename)
        {
            using (StreamWriter writer = File.CreateText(filename))
            {
                await new Writers.Writer(doc, writer).Write();
                doc.Filename = filename;
            }

           /* FileStream stream = new FileStream(filename, FileMode.Create,FileAccess.Write,FileShare.None,4096,true);
            TextWriter writer = new StreamWriter(stream);
            await new Writers.Writer(doc, writer).Write();
            writer.Close();
            stream.Close();*/

            

        }
    }
}
