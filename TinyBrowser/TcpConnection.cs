using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TinyBrowser{
    public class TcpConnection{
        Dictionary<int, string> sitesDictionary = new();

        const string MainSite = "www.Acme.com";
        const int Port = 80;

        int openNewLink = 0;
        List<string> sitesQue = new();

        UserInput userInput;
        public TcpConnection(){
            this.userInput = new UserInput(this.sitesDictionary);
            
        }

        public async void ConnectToSite(){
            this.sitesDictionary[Convert.ToInt32(this.openNewLink)] = "";
            this.sitesQue.Add("");
            while (true){
                var valueFromWeb = await SendRequest();

                Console.WriteLine($"User input: {this.openNewLink}");
                Console.WriteLine($"Dictionary: {this.sitesDictionary[Convert.ToInt32(this.openNewLink)]}");
                foreach (var (key, value) in this.sitesDictionary){
                    Console.WriteLine($"{key}  {value}");
                }

                if (Utilities.IsBadRequest(valueFromWeb)){
                    Utilities.PrintOutSites(this.sitesDictionary);
                    CustomOutputs.ConsoleWriteLine("Bad request, try again...", ConsoleColor.Red);
                }
                else{
                    SitesToDictionary(valueFromWeb);
                    Utilities.PrintOutSites(this.sitesDictionary);
                }
                
                this.openNewLink = this.userInput.GetUserChoice<int>();

                // if (this.openNewLink == "b"){
                //     Console.WriteLine(this.sitesQue.Count);
                //     this.openNewLink = (this.sitesQue.Count - 2).ToString();
                // }
                // else{
                //     if (!Utilities.IsBadRequest(valueFromWeb))
                //         this.sitesQue.Add(this.sitesDictionary[Convert.ToInt32(this.openNewLink)]);
                // }


                foreach (var s in this.sitesQue){
                    Console.WriteLine(s);
                }
            }
        }

        async Task<string> SendRequest(){
            var tcpClient = new TcpClient(MainSite, Port);
            var stream = tcpClient.GetStream();
            await stream.WriteAsync(
                Encoding.Default.GetBytes(Utilities
                    .Builder(MainSite, this.sitesDictionary[Convert.ToInt32(this.openNewLink)])
                    .ToString()));

            var bytes = new byte[tcpClient.ReceiveBufferSize];
            var receivedBytesLength = await stream.ReadAsync(bytes);
            var valueFromWeb = Encoding.Default.GetString(bytes).Remove(receivedBytesLength);
            // Console.WriteLine($"Site to reach: {Utilities.Builder(MainSite, this.sitesDictionary[this.userChoice])}");
            // Console.WriteLine(valueFromWeb);

            stream.Close();
            return valueFromWeb;
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

            Console.WriteLine($"First index in dic: {this.sitesDictionary[0]}");
        }
    }
}