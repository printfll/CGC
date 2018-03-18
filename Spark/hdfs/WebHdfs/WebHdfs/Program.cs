using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebHdfs;
namespace Microsoft.CGC
{
    class Program
    {
        static void Main(string[] args)
        {

            string host = "52.234.227.186";
            int port = 50070;
            HDFSClient service = new HDFSClient(host, port);
            service.Read("/user/helloworld.txt");
        }
    }
}
