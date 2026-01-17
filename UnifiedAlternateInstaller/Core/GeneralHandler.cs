namespace UnifiedAlternateInstaller.Core;

public class GeneralHandler
{
    public static void WriteColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static string GetTempPath()
    {
        return Environment.GetEnvironmentVariable("TEMP");
    }
    public static void CheckUAIFolder()
    {
        if (Directory.Exists($"{GetTempPath()}\\UAI"))
        {
            Directory.Delete($"{GetTempPath()}\\UAI", true);
            Directory.CreateDirectory($"{GetTempPath()}\\UAI");
            WriteColored("[*] New UAI folder created!", ConsoleColor.Gray);
        }
        else
        {
            WriteColored("[*] UAI folder doesn't exist! Creating...", ConsoleColor.Gray);
            Directory.CreateDirectory($"{GetTempPath()}\\UAI");
            WriteColored("[*] UAI folder created!", ConsoleColor.Gray);

        }
    }
}