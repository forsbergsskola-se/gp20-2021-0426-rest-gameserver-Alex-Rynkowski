using System;
using System.Net.Sockets;
using System.Text;

namespace TinyBrowser{
    static class Program{
        static void Main(string[] args){
            TMP();
            while (true){
                
            }
            return;
            var tcpConnection = new TcpConnection();
            tcpConnection.ConnectToSite();

            while (true){
            }
        }

        static async void TMP(){
            var site = "www.Milk.com";
            var tcpClient = new TcpClient(site, 80);
            var stream = tcpClient.GetStream();
            await stream.WriteAsync(Encoding.Default.GetBytes(Builder(site).ToString()));

            var bytes = new byte[tcpClient.ReceiveBufferSize];
            var receivedBytesLength = await stream.ReadAsync(bytes);
            var valueFromWeb = Encoding.Default.GetString(bytes).Remove(receivedBytesLength);
            Console.WriteLine(valueFromWeb);
            tcpClient.Close();
        }

        static StringBuilder Builder(string siteToConnectTo){
            var builder = new StringBuilder();
            builder.AppendLine($"GET /{siteToConnectTo}/barcode HTTP/1.1");
            builder.AppendLine($"Host: barcode");
            builder.AppendLine("Connection: close");
            builder.AppendLine();
            return builder;
        }
    }
}