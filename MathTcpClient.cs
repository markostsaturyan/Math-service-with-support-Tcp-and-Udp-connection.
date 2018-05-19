using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MathServiceClient
{
    public class MathTcpClient
    {
        public void ClientRun()
        {
            var client = new TcpClient(AddressFamily.InterNetwork);

            client.Connect(new IPEndPoint(IPAddress.Loopback, 3000));

            var serverStream = client.GetStream();

            var answer = new byte[4096];

            while (true)
            {
                var requset = Console.ReadLine();

                var buffer = Encoding.Unicode.GetBytes(requset);

                serverStream.Write(buffer, 0, buffer.Length);

                var lenght = serverStream.Read(answer, 0, 4096);

                Console.WriteLine(Encoding.Unicode.GetString(answer, 0, lenght));

            }
        }
    }
}
