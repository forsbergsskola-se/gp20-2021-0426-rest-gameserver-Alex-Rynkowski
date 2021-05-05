using System;

namespace TinyBrowser{
    static class Program{
        static void Main(string[] args){
            var tcpConnection = new TcpConnection();
            tcpConnection.ConnectToSite();

            while (true){
            }
        }
    }
}