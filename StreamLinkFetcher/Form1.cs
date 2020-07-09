using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamLinkFetcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btn_CopyToClipboard.Enabled = false;
            Text += $" {Assembly.GetExecutingAssembly().GetName().Version}";

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

                txt_TwitchLink.Enabled = true;
                btn_GetStreamUrl.Enabled = true;
                btn_Clear.Enabled = true;
            }
            catch (Exception ex)
            {
                lst_Diag.AppendText($"Contact riley... Something went wrong! You can try again but something did happen :( {ex.Message}{Environment.NewLine}");
                WriteToFile($"{ex.Message}\n\n{ex.StackTrace}");
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
            string command = $"streamlink {streamLink} best --stream-url --ringbuffer-size {ringBufferSize} | clip";

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
    }

    public class RingBufferSize
    {
        public string Text { get; set; }
    }
}
