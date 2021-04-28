using System;
using System.Net.Sockets;
using System.Text;

namespace TinyBrowser{
    static class Program{
        static void Main(string[] args){
            // TMP();
            // while (true){
            //     
            // }
            var tcpConnection = new TcpConnection();
            tcpConnection.ConnectToSite();

            while (true){
            }
        }

        static async void TMP(){
            var site = "www.acme.com";
            var tcpClient = new TcpClient("www.acme.com", 80);
            var stream = tcpClient.GetStream();
            await stream.WriteAsync(Encoding.Default.GetBytes(Builder(site).ToString()));
            var bytes = new byte[tcpClient.ReceiveBufferSize];
            var receivedBytesLength = await stream.ReadAsync(bytes);
            var valueFromWeb = Encoding.Default.GetString(bytes).Remove(receivedBytesLength);
            CustomOutputs.ConsoleWriteLine(valueFromWeb, ConsoleColor.Yellow);
            tcpClient.Close();
        }

        static StringBuilder Builder(string siteToConnectTo){
            var builder = new StringBuilder();
            CustomOutputs.ConsoleWriteLine($"{siteToConnectTo}", ConsoleColor.Cyan);
            builder.AppendLine($"GET /jef/paris_forts/ /1.1");
            builder.AppendLine($"Host: www.acme.com");
            builder.AppendLine("Connection: close");
            builder.AppendLine();
            return builder;
        }
    }
}