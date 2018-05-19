using System;

namespace MathServiceClient
{
    class ClientProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose Tcp or Udp.");

            var userChoose = Console.ReadLine();

            switch (userChoose)
            {
                case "Tcp":
                    {
                        var tcpClient = new MathTcpClient();

                        tcpClient.ClientRun();

                        break;
                    }
                case "Udp":
                    {
                        var udpClient = new MathUdpClient();

                        udpClient.ClientRun();

                        break;
                    }
            }
        }
    }
}
