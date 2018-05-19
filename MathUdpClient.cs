using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MathServiceClient
{
    public class MathUdpClient
    {
        public void ClientRun()
        {
            UdpClient client = new UdpClient(AddressFamily.InterNetwork);

            var endPoint = new IPEndPoint(IPAddress.Loopback, 2000);

            while (true)
            {
                var request = Console.ReadLine();

                var buffer = Encoding.Unicode.GetBytes(request);

                client.Send(buffer, buffer.Length, endPoint);

                var answer = client.Receive(ref endPoint);

                Console.WriteLine(Encoding.Unicode.GetString(answer, 0, answer.Length));
            }

        }
    }
}
