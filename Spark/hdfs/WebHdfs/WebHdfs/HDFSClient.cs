using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebHdfs;
namespace Microsoft.CGC
{
    class HDFSClient
    {
        private string prefix;
        private HttpClient client;

        public HDFSClient(string host, int port)
        {
             this.prefix = $"http://{host}:{port}/webhdfs/v1";
            this.client = new HttpClient();
        }

        public void Read(string path)
        {
            StringBuilder outputBuilder = new StringBuilder();
            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo("cmd.exe");
            info.Arguments = $"/c curl -v -i -L {this.prefix}{path}?op=OPEN";
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            process.StartInfo = info;
            // enable raising events because Process does not raise events by default
            process.EnableRaisingEvents = true;
            // attach the event handler for OutputDataReceived before starting the process
            process.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate (object sender, DataReceivedEventArgs e)
                {
        // append the new data to the data already read-in
        outputBuilder.Append(e.Data);
                }
            );
            // start the process
            // then begin asynchronously reading the output
            // then wait for the process to exit
            // then cancel asynchronously reading the output
            process.Start();
            Console.WriteLine("start");

            process.WaitForExit(1000 * 60 * 5);
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();

            // use the output
            string output = outputBuilder.ToString();
            Console.WriteLine(output);
        }
    }
}
