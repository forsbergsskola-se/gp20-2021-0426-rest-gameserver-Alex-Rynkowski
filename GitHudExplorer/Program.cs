using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TinyBrowser;

namespace GitHudExplorer{
    class Program{
        static void Main(string[] args){
            var explorer = new WowExplorer();
            
            explorer.AddLinksToDictionary();
            while (true){
            }
        }

        
    }
}