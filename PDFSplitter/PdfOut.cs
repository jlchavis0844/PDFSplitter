using System.IO;

namespace PDFSplitter {
    internal class PdfOut {
        public string fileName { get; set; }
        public string extension { get; set; }
        public string path { get; set; }
        public string outFile { get; set; }
        public int startPage { get; set; }
        public int endPage { get; set; }
        public int rotate { get; set; }

        public PdfOut(string file, int start, int end, string type, int turn) {
            fileName = Path.GetFileNameWithoutExtension(file);
            extension = Path.GetExtension(file);
            path = Path.GetDirectoryName(file);
            outFile = path + "\\" + fileName + "_" + type + extension;
            startPage = start;
            endPage = end;
            rotate = turn;
        }
    }
}