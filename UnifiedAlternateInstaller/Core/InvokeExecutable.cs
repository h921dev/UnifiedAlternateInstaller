using System.Diagnostics;

namespace UnifiedAlternateInstaller.Core;

public class InvokeExecutable
{
    static void Invoker(string application = "", string args = "")
    {
        if (application == "") return;
        string finalLocation = $"%TEMP%\\UAI\\{application}";
        if (!File.Exists(finalLocation))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"File {finalLocation} not found!");
            Console.ResetColor();
            return;
        }
        ProcessStartInfo executable = new ProcessStartInfo(finalLocation);
        executable.CreateNoWindow = true;
        executable.Arguments = args;
        Process.Start(executable);
    }
}