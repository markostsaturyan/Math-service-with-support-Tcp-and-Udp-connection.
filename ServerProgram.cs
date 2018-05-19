using System.Threading.Tasks;

namespace MathService
{
    class ServerProgram
    {
        static void Main(string[] args)
        {
            Task.Run(() =>
            {
                var udpServer = new MathUdpServer();

                udpServer.ServerRun();
            });

            var tcpServer = new MathTcpServer();

            tcpServer.ServerRun();
        }
    }
}
