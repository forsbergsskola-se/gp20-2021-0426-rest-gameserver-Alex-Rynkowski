using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TinyBrowser{
    public class TcpConnection{
        readonly Dictionary<int, string> sitesDictionary = new();

        const string MainSite = "www.Milk.com/mimu";
        const int Port = 80;

        public async void ConnectToSite(){
            var valueFromWeb = await ConnectionToMainSite();

            SitesToDictionary(valueFromWeb);

            OpenNewLink();
        }

        async void OpenNewLink(){
            while (true){
                PrintOutSites();

                var userChoice = GetUserChoice();

                var tcpClient = new TcpClient(MainSite, Port);
                var stream = tcpClient.GetStream();

                var builder = Builder(this.sitesDictionary[userChoice]);
                await stream.WriteAsync(Encoding.Default.GetBytes(builder.ToString()));
                var bytes = new byte[tcpClient.ReceiveBufferSize];
                var receivedBytesLength = await stream.ReadAsync(bytes);
                var valueFromWeb = Encoding.Default.GetString(bytes).Remove(receivedBytesLength);
                Console.WriteLine(valueFromWeb);
                Console.WriteLine(builder);
                tcpClient.Close();
            }
        }

        int GetUserChoice(){
            int userChoice;
            while (true){
                CustomOutputs.ConsoleWriteLine($"Type in a number between 0-{this.sitesDictionary.Count - 1}",
                    ConsoleColor.Green);
                var userInput = CustomOutputs.ConsoleReadLine(ConsoleColor.Yellow);


                if (ValidateUserInput(userInput, out userChoice)){
                    Console.WriteLine("Wrong input, try again ");
                    continue;
                }

                break;
            }

            return userChoice;
        }

        bool ValidateUserInput(string userInput, out int userChoice){
            var result = int.TryParse(userInput, NumberStyles.Integer, new NumberFormatInfo(), out userChoice);
            return userChoice < 0 || userChoice > this.sitesDictionary.Count - 1 || !result;
        }

        void PrintOutSites(){
            foreach (var (key, value) in this.sitesDictionary){
                //Console.WriteLine($"{key} {value}");
                CustomOutputs.ConsoleWriteLine($"%{key}% %{value} %({value})%");
            }
        }

        static async Task<string> ConnectionToMainSite(){
            var tcpClient = new TcpClient(MainSite, Port);
            var stream = tcpClient.GetStream();
            await stream.WriteAsync(Encoding.Default.GetBytes(Builder(MainSite).ToString()));

            var bytes = new byte[tcpClient.ReceiveBufferSize];
            var receivedBytesLength = await stream.ReadAsync(bytes);
            var valueFromWeb = Encoding.Default.GetString(bytes).Remove(receivedBytesLength);
            Console.WriteLine(valueFromWeb);
            tcpClient.Close();
            return valueFromWeb;
        }

        static StringBuilder Builder(string siteToConnectTo){
            var builder = new StringBuilder();
            builder.AppendLine("GET / HTTP/1.1");
            builder.AppendLine($"Host: {siteToConnectTo}");
            builder.AppendLine("Connection: close");
            builder.AppendLine();
            return builder;
        }

        void SitesToDictionary(string valueFromWeb){
            var textStartIndex = 0;
            var txtStartIndex = 0;
            var siteIndex = 0;
            var title = FindTextBetween(valueFromWeb, "<title>", "</title>", ref textStartIndex);

            this.sitesDictionary[siteIndex] = title;

            while (true){
                try{
                    var foundLink = FindTextBetween(valueFromWeb, "<a href=\"", "\"", ref txtStartIndex);
                    siteIndex++;
                    if (foundLink.StartsWith("http")){
                        this.sitesDictionary[siteIndex] = $"{MainSite}/{foundLink}";
                        continue;
                    }

                    this.sitesDictionary[siteIndex] = $"{foundLink}";
                }
                catch (Exception e){
                    Console.WriteLine("End of the line");
                    break;
                }
            }
        }

        static string FindTextBetween(string textToSearch, string startText, string endText, ref int startAtIndex){
            var startIndex = textToSearch.IndexOf(startText, startAtIndex, StringComparison.Ordinal) + startText.Length;
            var endIndex = textToSearch.IndexOf(endText, startIndex + 2, StringComparison.Ordinal);

            startAtIndex = endIndex + 1;
            var newText = textToSearch.Remove(endIndex).Substring(startIndex);

            return newText;
        }
    }
}