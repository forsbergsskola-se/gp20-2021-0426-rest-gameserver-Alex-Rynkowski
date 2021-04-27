using System;
using System.Net.Sockets;
using System.Text;

namespace TinyBrowser{
    static class Program{
        static void Main(string[] args){
            Console.WriteLine($"A quote...  \"");
            TcpConnection.ConnectToSite();

            while (true){
            }
        }
    }
}