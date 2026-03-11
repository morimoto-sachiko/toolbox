using System.Runtime.InteropServices;

public static class JsonFormatterNative
{
    [DllImport("JsonFormatterDLL.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int FormatJsonFile(
        string input,
        string output);
}



// よびだし
int result = JsonFormatterNative.FormatJsonFile(
    "temp.json",
    "enemy.json");

if (result != 0)
{
    Console.WriteLine("format error");
}