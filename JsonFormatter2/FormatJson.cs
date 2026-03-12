using System.Diagnostics;

public static void FormatJson(string input, string output)
{
    var p = new Process();

    p.StartInfo.FileName = "JsonFormatter.exe";
    p.StartInfo.Arguments = $"{input} {output}";
    p.StartInfo.UseShellExecute = false;

    p.Start();
    p.WaitForExit();
}