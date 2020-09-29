using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamLinkFetcher
{
    /*
     * Note: I am super aware this code is messy and doesn't show my BEST work
     *       but I wrote this for a one off event and have done a handful of tiny
     *       patches to it for the Unison Team. One day I might actually clean it up
     *       and make it cross platform etc, but for now it works...
     */
    public partial class Form1 : Form
    {
        StreamLinkType slType = StreamLinkType.NONE;
        bool isNested = false;

        public Form1()
        {
            InitializeComponent();
            btn_CopyToClipboard.Enabled = false;
            Text += $" {Assembly.GetExecutingAssembly().GetName().Version}";

            slType = CheckForStreamLinkType();

            if (slType == StreamLinkType.NONE)
            {
                // Safety
                txt_TwitchLink.Enabled = false;
                cmb_RingBuffer.Enabled = false;
                txt_StreamVideoLink.Enabled = false;
                btn_GetStreamUrl.Enabled = false;
                btn_Clear.Enabled = false;

                // Go install streamlink
                MessageBox.Show(
                    $"We haven't detected Streamlink on this system, please use either the portable version or the CLI version.{Environment.NewLine}Application will close when you click OK",
                    "Missing StreamLink",
                    MessageBoxButtons.OK);

                // Bye bye
                Environment.Exit(1);
            }

            // I really don't know what this does but makes things easier
            var ringBufferSizes = new RingBufferSize[]
            {
                new RingBufferSize{ Text = "128K" },
                new RingBufferSize{ Text = "256K" },
                new RingBufferSize{ Text = "512K" },
                new RingBufferSize{ Text = "1M" },
                new RingBufferSize{ Text = "2M" },
                new RingBufferSize{ Text = "4M" },
                new RingBufferSize{ Text = "8M" },
                new RingBufferSize{ Text = "16M" }
            };

            cmb_RingBuffer.ValueMember = "ID";
            cmb_RingBuffer.DisplayMember = "Text";
            cmb_RingBuffer.DataSource = ringBufferSizes;
            cmb_RingBuffer.SelectedIndex = ringBufferSizes.Length - 1;
        }

        private async void btn_GetStreamUrl_Click(object sender, EventArgs e)
        {
            txt_StreamVideoLink.Text = "";

            try
            {
                if (string.IsNullOrEmpty(txt_TwitchLink.Text))
                    return;

                string streamUrl = txt_TwitchLink.Text;

                // We don't want to accidentally append things!
                if (!streamUrl.Contains("https://") && streamUrl.IndexOf("twitch.tv", 0, StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    streamUrl = $"https://{streamUrl}";
                }
                else if (!streamUrl.Contains("https://") && streamUrl.IndexOf("twitch.tv", 0, StringComparison.InvariantCultureIgnoreCase) == -1)
                {
                    streamUrl = $"https://twitch.tv/{streamUrl}";
                }

                txt_TwitchLink.Enabled = false;
                btn_GetStreamUrl.Enabled = false;
                btn_Clear.Enabled = false;

                lst_Diag.AppendText($"Fetching video url for {streamUrl}... Please wait{Environment.NewLine}");

                RingBufferSize bufferSize = (RingBufferSize)cmb_RingBuffer.SelectedItem;

                // Await so we don't do things we don't want happening before we have the output
                await Task.Run(() => BuildCommand(streamUrl, bufferSize.Text));
                var fetchedUrl = Clipboard.GetText().Trim();

                if (fetchedUrl != null)
                {
                    Clipboard.SetText(fetchedUrl);
                    if (fetchedUrl.Contains("error"))
                    {
                        lst_Diag.AppendText($"Fetch Failed with message: {fetchedUrl}{Environment.NewLine}");
                    }
                    else if (!fetchedUrl.Contains("https://") || !fetchedUrl.Contains("m3u8"))
                    {
                        lst_Diag.AppendText($"We didn't get back a URL, are you sure streamlink is installed??{Environment.NewLine}");
                    }
                    else
                    {
                        txt_StreamVideoLink.Text = fetchedUrl;
                        lst_Diag.AppendText($"Fetched Successfully!{Environment.NewLine}");
                        btn_CopyToClipboard.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lst_Diag.AppendText($"Contact riley... Something went wrong! You can try again but something did happen :( {ex.Message}{Environment.NewLine}");
                WriteToFile($"{ex.Message}\n\n{ex.StackTrace}");
            }
            finally
            {
                // Re-enable cause...Might be a fluke it didn't work so allow for re-tries
                txt_TwitchLink.Enabled = true;
                btn_GetStreamUrl.Enabled = true;
                btn_Clear.Enabled = true;
            }

            lst_Diag.AppendText("\n");
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_TwitchLink.Clear();
            txt_StreamVideoLink.Clear();

            WriteLogToFile();

            lst_Diag.Clear();

            GC.Collect();

            btn_CopyToClipboard.Enabled = false;
        }

        private void btn_CopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txt_StreamVideoLink.Text);
        }

        void WriteLogToFile()
        {
            WriteToFile(lst_Diag.Text);
        }

        void WriteToFile(string data)
        {
            File.AppendAllText(@".\\StreamExtractorLog.txt", $"{DateTime.Now}{Environment.NewLine}----{Environment.NewLine}{data}----{Environment.NewLine}{Environment.NewLine}");
        }

        void BuildCommand(string streamLink, string ringBufferSize)
        {
            // TODO (riley): Implement this
            // If we call streamlink {streamLink} --json we can get back all the sizes of video
            // We can mod this call with --json to get data back as a JSON object!
            
            string streamLinkExe = "";

            if (slType == StreamLinkType.PORTABLE)
            {
                if (isNested)
                {
                    streamLinkExe = "streamlink\\streamlink.bat";
                }
                else
                {
                    streamLinkExe = "streamlink.bat";
                }
            }
            else if (slType == StreamLinkType.CLI)
            {
                streamLinkExe = "streamlink";
            }

            string command = $"{streamLinkExe} {streamLink} best --stream-url --ringbuffer-size {ringBufferSize} | clip";

            Invoke(
                new MethodInvoker(
                    delegate ()
                    {
                        lst_Diag.AppendText($"Running command: {command}{Environment.NewLine}");
                    }
                )
            );

            // Call out to StreamLink
            RunCommand(command);
        }

        void RunCommand(string command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $"/C {command}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            process.StartInfo = startInfo;
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                Debug.WriteLine(process.StandardOutput.ReadLine());
            }
            process.WaitForExit();

            GC.Collect();
        }

        StreamLinkType CheckForStreamLinkType()
        {
            var currentLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (File.Exists(Path.Combine(currentLocation, "Streamlink.exe")) || File.Exists(Path.Combine(currentLocation, "Streamlink.bat")))
            {
                return StreamLinkType.PORTABLE;
            }

            if (
                File.Exists(Path.Combine(currentLocation, "Streamlink", "Streamlink.exe")) ||
                File.Exists(Path.Combine(currentLocation, "Streamlink", "Streamlink.bat"))
            )
            {
                isNested = true;
                return StreamLinkType.PORTABLE;
            }

            if (ExistsOnPath("streamlink.exe"))
            {
                return StreamLinkType.CLI;
            }

            return StreamLinkType.NONE;
        }

        // https://stackoverflow.com/a/3856090 StackOverflow is so good sometimes
        public static bool ExistsOnPath(string fileName)
        {
            return GetFullPath(fileName) != null;
        }

        public static string GetFullPath(string fileName)
        {
            if (File.Exists(fileName))
                return Path.GetFullPath(fileName);

            var values = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in values.Split(Path.PathSeparator))
            {
                var fullPath = Path.Combine(path, fileName);
                if (File.Exists(fullPath))
                    return fullPath;
            }
            return null;
        }
    }

    public class RingBufferSize
    {
        public string Text { get; set; }
    }

    public enum StreamLinkType
    {
        CLI,
        PORTABLE,
        NONE
    }
}
