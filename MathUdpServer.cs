using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MathService
{
    public class MathUdpServer:IMathService
    {
        public void ServerRun()
        {
            var clientEndPoint = new IPEndPoint(IPAddress.Any, 2000);

            UdpClient server = new UdpClient(clientEndPoint);

            while (true)
            {
                var request = server.Receive(ref clientEndPoint);

                Task.Run(() => ServeClients(server, clientEndPoint, request));
            }
        }

        private void ServeClients(UdpClient server, IPEndPoint endPoint, byte[] requset)
        {
            Func<double, double, double> operation=null;

            var requsetString = Encoding.Unicode.GetString(requset);

            var opAndValues = requsetString.Split(':');

            var firstValue = Convert.ToDouble(opAndValues[1]);

            var secondValue = Convert.ToDouble(opAndValues[2]);

            switch (opAndValues[0])
            {
                case "+":
                    {
                        operation = Add;
                        break;
                    }
                case "-":
                    {
                        operation = Sub;
                        break;
                    }
                case "*":
                    {
                        operation = Mult;
                        break;
                    }
                case "/":
                    {
                        operation = Div;
                        break;
                    }
            }

            if (operation != null)
            {
                var result = operation(firstValue, secondValue);

                var resultString = result.ToString();

                var resultBytes = Encoding.Unicode.GetBytes(resultString);

                server.Send(resultBytes, resultBytes.Length, endPoint);
            }
            else
            {
                var errorString = "Invalid operation";

                var errorBytes = Encoding.Unicode.GetBytes(errorString);

                server.Send(errorBytes, errorBytes.Length, endPoint);
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
