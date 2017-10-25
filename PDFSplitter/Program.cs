using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace PDFSplitter {

    class Program {
        static void Main(string[] args) {
            //args = new string[] { "c:\\testing\\RALIM_Ann_101117.pdf", "5", "30", "comm", "0", "31", "40", "advance", "90" };
            if(args[0] == "-h") {
                Console.WriteLine("Params as follows: \n\t[0] Full path to source pdf");
                Console.WriteLine("\tparams [n+1...n+4]:\n\t\t[1] Start page\n\t\t[2] End page (inclusive)");
                Console.WriteLine("\t\t[3] Name appended to file (ie comm, avd)\n\t\t[4] Rotation amount (clockwise");
                Console.WriteLine("Example: \"PDFsplitter.exe c:\\testing\\RALIM_Ann_101117.pdf 5 30 comm 0 31 40 advance 90\"");
                Console.WriteLine("Example Output: RALIM_Ann_101117_comm.pdf(pgs 5-30, no rotation) and RALIM_Ann_101117_advance.pdf (pgs 31-40, 90deg rotation)");
                System.Environment.Exit(1);
            }

            if ((args.Length - 1) % 4 != 0) {
                Console.WriteLine("Wrong Amount of arguments");
                Microsoft.VisualBasic.Interaction.MsgBox("Wrong Amount of Arguments");
                System.Environment.Exit(-1);
            }
            List<PdfOut> outs = new List<PdfOut>();

            for (int i = 1; i < args.Length; i += 4) {

                outs.Add(new PdfOut(args[0], Convert.ToInt32(args[i]), Convert.ToInt32(args[i + 1]), args[i + 2], Convert.ToInt32(args[i + 3])));
            }

            for (int i = 0; i < outs.Count; i++) {
                PdfReader reader = new PdfReader(args[0]);
                Document document = new Document(reader.GetPageSizeWithRotation(outs[i].endPage));

                PdfCopy copy = new PdfCopy(document, new System.IO.FileStream(outs[i].outFile, System.IO.FileMode.Create));
                document.Open();


                PdfDictionary page;
                PdfNumber rotate;
                for (int j = outs[i].startPage; j <= outs[i].endPage; j++) {
                    page = reader.GetPageN(j);
                    rotate = page.GetAsNumber(PdfName.ROTATE);
                    page.Put(PdfName.ROTATE, new PdfNumber(outs[i].rotate));
                    copy.AddPage(copy.GetImportedPage(reader, j));
                }
                document.Close();
                reader.Close();
            }
        }
    }
}
