using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TinyBrowser{
    public class TcpConnection{
        Dictionary<int, string> sitesDictionary = new();

        const string MainSite = "www.Acme.com";
        const int Port = 80;

        int openNewLink = 0;
        string openPreviousLink = "";
        readonly UserInput userInput;

        public TcpConnection(){
            this.userInput = new UserInput(this.sitesDictionary);
        }

        public async void ConnectToSite(){
            this.sitesDictionary[Convert.ToInt32(this.openNewLink)] = "";
            while (true){
                var valueFromWeb = await SendRequest();

                if (Utilities.IsBadRequest(valueFromWeb)){
                    Utilities.PrintOutSites(this.sitesDictionary);
                    CustomOutputs.ConsoleWriteLine("Bad request, try again...", ConsoleColor.Red);
                }
                else{
                    SitesToDictionary(valueFromWeb);
                    Utilities.PrintOutSites(this.sitesDictionary);
                }

                if (this.userInput.GetUserChoice(out var value)){
                    this.openNewLink = (int) value;
                }
                else{
                    this.openPreviousLink = (string) value;
                }
            }
        }

        async Task<string> SendRequest(){
            var tcpClient = new TcpClient(MainSite, Port);
            var stream = tcpClient.GetStream();
            await WriteToSite(stream);
            var bytes = new byte[tcpClient.ReceiveBufferSize];
            var receivedBytesLength = await stream.ReadAsync(bytes);
            Console.WriteLine("Made it here");
            var valueFromWeb = Encoding.Default.GetString(bytes).Remove(receivedBytesLength);
            stream.Close();
            return valueFromWeb;
        }

        async Task WriteToSite(Stream stream){
            if (this.openPreviousLink.StartsWith('b')){
                Console.WriteLine(MainSite + this.openPreviousLink.Substring(1));
                await stream.WriteAsync(Encoding.Default.GetBytes(MainSite + this.openPreviousLink.Substring(1)));
                return;
            }

            await stream.WriteAsync(Encoding.Default.GetBytes(Utilities
                .Builder(MainSite, this.sitesDictionary[this.openNewLink]).ToString()));
        }

        void SitesToDictionary(string valueFromWeb){
            this.sitesDictionary = new Dictionary<int, string>{[0] = ""};
            var textStartIndex = 0;
            var txtStartIndex = 0;
            var siteIndex = 1;
            var title = Utilities.FindTextBetween(valueFromWeb, "<title>", "</title>", ref textStartIndex);

            this.sitesDictionary[siteIndex] = title;

            while (true){
                var foundLink = Utilities.FindTextBetween(valueFromWeb, "<a href=\"", "\"", ref txtStartIndex);

                if (foundLink == "") break;

                if (foundLink.StartsWith('/')){
                    foundLink = foundLink.Substring(1);
                }

                siteIndex++;
                if (foundLink.StartsWith("http")){
                    this.sitesDictionary[siteIndex] = $"{MainSite}/{foundLink}";
                    continue;
                }

                this.sitesDictionary[siteIndex] = $"{foundLink}/";
            }
        }
    }
}