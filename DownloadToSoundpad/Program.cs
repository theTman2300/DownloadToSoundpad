// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using SoundpadConnector;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

internal class Program
{
    private static async Task Main(string[] args)
    {
        while (true)
        {

            var startup = new Startup();
            string path;

            Console.WriteLine("Input youtube link or type 'E' to exit ...");
            string input = Console.ReadLine();
            if (input == "e" || input == "E")
            {
                Environment.Exit(0);
                return;
            }

            /*----- Download Sound ----- */
            var ytdl = new YoutubeDL
            {
                YoutubeDLPath = startup.Settings.ytdlpPath,
                FFmpegPath = startup.Settings.ffmpegPath,
                OutputFolder = startup.Settings.DownloadDirectory,
                OutputFileTemplate = startup.Settings.outputFileTemplate,
                RestrictFilenames = true
            };

            Console.WriteLine("Updating ytdl...");
            await ytdl.RunUpdate();

            Console.WriteLine("Downloading Video...");
            RunResult<string> res;
            try
            {
                res = await ytdl.RunAudioDownload(input, AudioConversionFormat.Mp3);
            }
            catch
            {
                Console.WriteLine("Download Failed\n\n");
                continue;
            }

            if (!res.Success)
            {
                Console.WriteLine("Download Failed");
                foreach (string errorText in res.ErrorOutput)
                {
                    Console.WriteLine(errorText);
                }
                Console.WriteLine("\n\n");
                continue;
            }
            path = res.Data;
            Console.WriteLine("Downloaded to: " + path);

            /*----- Import to Soundpad ----- */
            Console.WriteLine("Connecting to SoundPad...");
            Soundpad soundPad = new();
            await soundPad.ConnectAsync();

            var thing = await soundPad.GetSoundFileCount();
            int soundCount = (int)thing.Value;
            Console.WriteLine("Adding sound at index " + soundCount);
            await soundPad.AddSound(path, soundCount); // add sound

            Console.WriteLine("Done!\n");
        }
    }
}

public class Startup
{
    public Startup()
    {
        var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("Settings.json", optional: false);

        IConfiguration config = builder.Build();

        Settings = config.GetSection("Settings").Get<Settings>();

    }

    public Settings Settings { get; private set; }
}

public class Settings
{
    public string DownloadDirectory { get; set; }
    public string ytdlpPath { get; set; }
    public string ffmpegPath { get; set; }
    public string outputFileTemplate { get; set; }
}