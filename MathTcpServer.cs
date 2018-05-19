using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MathService
{
    public class MathTcpServer:IMathService
    {
        public void ServerRun()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 3000);

            tcpListener.Start(100);

            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                Task.Run(() => ServeClients(client));
            }

        }

        private void ServeClients(TcpClient client)
        {
            var stream = client.GetStream();

            var buffer = new byte[4096];

            while (true)
            {
                var lenght = stream.Read(buffer, 0, 4096);

                var request = Encoding.Unicode.GetString(buffer, 0, lenght);

                var opAndValues = request.Split(':');

                var firstValue = Convert.ToDouble(opAndValues[1]);

                var secondValue = Convert.ToDouble(opAndValues[2]);

                switch (opAndValues[0])
                {
                    case "+":
                        {
                            var result = Add(firstValue, secondValue);

                            var resultString = result.ToString();

                            var resultBytes = Encoding.Unicode.GetBytes(resultString);

                            stream.Write(resultBytes, 0, resultBytes.Length);

                            break;
                        }
                    case "-":
                        {
                            var result = Sub(firstValue, secondValue);

                            var resultString = result.ToString();

                            var resultBytes = Encoding.Unicode.GetBytes(resultString);

                            stream.Write(resultBytes, 0, resultBytes.Length);

                            break;
                        }
                    case "*":
                        {
                            var result = Mult(firstValue, secondValue);

                            var resultString = result.ToString();

                            var resultBytes = Encoding.Unicode.GetBytes(resultString);

                            stream.Write(resultBytes, 0, resultBytes.Length);

                            break;
                        }
                    case "/":
                        {
                            var result = Div(firstValue, secondValue);

                            var resultString = result.ToString();

                            var resultBytes = Encoding.Unicode.GetBytes(resultString);

                            stream.Write(resultBytes, 0, resultBytes.Length);

                            break;
                        }
                }

            }

        }

        public double Add(double firstValue, double secondValue)
        {
            return firstValue + secondValue;
        }

        public double Div(double firstValue, double secondValue)
        {
            return firstValue / secondValue;
        }

        public double Mult(double firstValue, double secondValue)
        {
            return firstValue * secondValue;
        }

        public double Sub(double firstValue, double secondValue)
        {
            return firstValue - secondValue;
        }
    }
}
