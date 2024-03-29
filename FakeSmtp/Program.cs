using System;
using System.Net;
using System.Threading;
using System.Linq;

namespace FakeSmtp
{
    public class SMTPServer
    {
        [STAThread] 
        static void Main(string[] args)
        {
            SMTPServer server = new SMTPServer();
            bool outputToFile = args.All(a => a != "--no-files");
            server.RunServer(outputToFile);
        }

        public void RunServer(bool outputToFile)
        {
            MailListener listener = null;
            int port = 25;

            do
            {
                Console.WriteLine("New MailListener started on port {0}.", port);
                listener = new MailListener(this, IPAddress.Loopback, port);
                listener.OutputToFile = outputToFile;
                listener.Start();
                while (listener.IsThreadAlive)
                {
                    Thread.Sleep(500);
                }
            } while (listener != null);

        }
    }
}
