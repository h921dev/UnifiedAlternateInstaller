using System.Management;

namespace UnifiedAlternateInstaller.Hardware;

public class CpuDetector
{
    public static string GetCpuVendor()
    {
        using var searcher = new ManagementObjectSearcher(
            "SELECT Manufacturer, Name FROM Win32_Processor");

        foreach (ManagementObject mo in searcher.Get())
        {
            string manufacturer = mo["Manufacturer"]?.ToString() ?? "";

            if (manufacturer.Contains("Intel")) return "INTEL";
            if (manufacturer.Contains("AMD")) return "AMD";
        }

        return "UNKNOWN";
    }
}