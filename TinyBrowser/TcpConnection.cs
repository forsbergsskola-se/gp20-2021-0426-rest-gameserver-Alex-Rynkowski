using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace TinyBrowser{
    public static class TcpConnection{
        public static async void ConnectToSite(){
            var set = new HashSet<string>();
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
            var valueFromWeb = Encoding.Default.GetString(bytes).Remove(tmp);
            Console.WriteLine(FindTextBetween(valueFromWeb, "<title>", "</title>"));

            var myInt = 0;
            while (true){
                try{
                    var foundText = FindTextBetween(valueFromWeb, "<a href=\"", "\"", ref myInt);
                    set.Add(foundText);
                    Console.WriteLine(foundText);
                }
                catch (Exception e){
                    Console.WriteLine("End of the line");
                    break;
                }
            }


            //Console.WriteLine(returnedString);
            tcpClient.Close();
        }

        static string FindTextBetween(string textToSearch, string startText, string endText){
            var startIndex = textToSearch.IndexOf(startText, StringComparison.Ordinal) + startText.Length;
            var endIndex = textToSearch.IndexOf(endText, StringComparison.Ordinal);

            var newText = textToSearch.Remove(endIndex).Substring(startIndex);
            return newText;
        }

        static string FindTextBetween(string textToSearch, string startText, string endText, ref int startAt){
            var startIndex = textToSearch.IndexOf(startText, startAt, StringComparison.Ordinal) + startText.Length;
            var endIndex = textToSearch.IndexOf(endText, startIndex + 2, StringComparison.Ordinal);

            startAt = endIndex + 1;
            var newText = textToSearch.Remove(endIndex).Substring(startIndex);

            return newText;
        }
    }
}