using UnifiedAlternateInstaller.Hardware;
using UnifiedAlternateInstaller.Core;

static class Program
{
    static string version = "1.0.0";
    static bool hasInitialized = false;
    
    static void showIntroCard()
    {
        GeneralHandler.WriteColored("=============================", ConsoleColor.Red);
        GeneralHandler.WriteColored("Unified Alternate Installer", ConsoleColor.Red);
        GeneralHandler.WriteColored($"Version: {version}", ConsoleColor.Red);
        GeneralHandler.WriteColored("with <3 from h921.cc", ConsoleColor.Red);
        GeneralHandler.WriteColored("=============================", ConsoleColor.Red);
    }

    static void initializer()
    {
        GeneralHandler.WriteColored("Initializing...", ConsoleColor.Gray);
        GeneralHandler.CheckUAIFolder();
        GeneralHandler.WriteColored("Initialized!", ConsoleColor.Gray);
        Thread.Sleep(1000);
        showIntroCard();
    }
    static void Main(string[] args)
    {
        initializer();
        List<string> toInstall = new List<string>();
        List<(string Name, string Vendor, string PciId)> GotGPUs = GpuDetector.GetGpus();
        for (int i = 0; i < GotGPUs.Count; i++)
        {
            GeneralHandler.WriteColored($"New GPU detected! -> {GotGPUs[i].Name}", ConsoleColor.Green);
            if (GotGPUs[i].Vendor == "NVIDIA") toInstall.Add("NvidiaDriver");
            if (GotGPUs[i].Vendor == "AMD") toInstall.Add("AmdDriver");
        }

        if (CpuDetector.GetCpuVendor() == "INTEL")
        {
            GeneralHandler.WriteColored("Intel CPU detected!", ConsoleColor.Green);
            toInstall.Add("IntelChipsets");
        }
        
        
        
    }
}