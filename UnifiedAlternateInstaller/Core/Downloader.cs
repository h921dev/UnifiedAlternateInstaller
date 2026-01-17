using System.Net;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnifiedAlternateInstaller.Core;
using static GeneralHandler;

public class Downloader
{
    private static readonly HttpClient client = new HttpClient();
    static string getDownloadUrl(string application)
    {
        string[] applicationNames = { "NvidiaDriver", "AmdDriver" };
        string[] applicationURLs =
        {
            "https://uk.download.nvidia.com/nvapp/client/11.0.6.379/NVIDIA_app_v11.0.6.379.exe",
            "https://drivers.amd.com/drivers/installer/25.20/whql/amd-software-adrenalin-edition-25.12.1-minimalsetup-251207_web.exe"

        };
        for (int i=0;i<applicationNames.Length;i++)
        {
            if (applicationNames[i] == application) return applicationURLs[i];
        }
        return "";
    }
    public static async Task<bool> InvokeDownload(string url, string fileName)
    {
        try
        {
            using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var fileStream = new FileStream($"{GetTempPath()}\\UAI\\{fileName}",FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

            await stream.CopyToAsync(fileStream);
            return true;
        }
        catch (Exception e)
        {
            GeneralHandler.WriteColored($"Download mislukt! {e.Message}", ConsoleColor.Red);
            return false;
        }
    }
}