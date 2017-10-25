using System;

public class PdfCout
{
    public string fileName { get; set; }
    public string extension { get; set; }
    public string path { get; set; }
    public string outFile { get; set; }
    public int start { get; set; }
    public int end { get; set; }

    public PdfCout(string file, int start, int end, string type)
	{
        fileName = Path.GetFileNameWithoutExtension(file);
        extension = Path.GetExtension(file);
        path = Path.GetDirectoryName(args[0]);
        outFile = path + "\\" + fileName + "_" + type + "." + extension;
        startPage = start;
        endPage = end;
    }
}
