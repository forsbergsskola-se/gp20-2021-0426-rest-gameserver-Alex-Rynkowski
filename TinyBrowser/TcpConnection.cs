using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TinyBrowser{
    public static class TcpConnection{
        public static async void ConnectToSite(){
                var tcpClient = new TcpClient("www.Milk.com", 80);

                var stream = tcpClient.GetStream();
                var builder = new StringBuilder();
                builder.AppendLine("GET / HTTP/1.1");
                builder.AppendLine("Host: www.Milk.com");
                builder.AppendLine("Connection: close");
                builder.AppendLine();
                Console.WriteLine(builder.ToString());
                await stream.WriteAsync(Encoding.Default.GetBytes(builder.ToString()));

                var bytes = new byte[tcpClient.ReceiveBufferSize];
                var tmp = await stream.ReadAsync(bytes);
                var returnedString = Encoding.Default.GetString(bytes).Remove(tmp);
                Console.WriteLine(returnedString);
                tcpClient.Close();
        }
    }
}