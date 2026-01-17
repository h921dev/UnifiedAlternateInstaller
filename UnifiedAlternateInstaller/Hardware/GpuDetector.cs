namespace UnifiedAlternateInstaller.Hardware;
using System.Management;


public class GpuDetector
{
    public static List<(string Name, string Vendor, string PciId)> GetGpus()
    {
        var gpus = new List<(string, string, string)>();

        using var searcher = new ManagementObjectSearcher(
            "SELECT Name, PNPDeviceID FROM Win32_VideoController");

        foreach (ManagementObject mo in searcher.Get())
        {
            string name = mo["Name"]?.ToString() ?? "Unknown";
            string pnpId = mo["PNPDeviceID"]?.ToString() ?? "";

            string vendor = pnpId switch
            {
                var s when s.Contains("VEN_10DE") => "NVIDIA",
                var s when s.Contains("VEN_1002") => "AMD",
                var s when s.Contains("VEN_8086") => "INTEL",
                _ => "UNKNOWN"
            };

            gpus.Add((name, vendor, pnpId));
        }

        return gpus;
    }
}