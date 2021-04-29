using System;
using System.Collections.Generic;
using System.Globalization;

namespace TinyBrowser{
    public class UserInput{
        readonly Dictionary<int, string> sitesDictionary;

        Dictionary<int, string> sitesQue = new();

        public UserInput(Dictionary<int, string> sitesDictionary){
            this.sitesDictionary = sitesDictionary;
            this.sitesQue[0] = "";
        }

        public int CurrentSiteIndex{ get; set; }

        public void AddSiteToQue(string site){
            this.CurrentSiteIndex++;
            this.sitesQue[this.CurrentSiteIndex] = site;
        }

        public void RemoveSiteFromQue(){
            if (this.CurrentSiteIndex == 0) return;
            this.sitesQue.Remove(this.CurrentSiteIndex);
            this.CurrentSiteIndex--;
        }

        public bool GetUserChoice(out object value){
            while (true){
                CustomOutputs.ConsoleWriteLine(
                    $"Type in a number between 0-{this.sitesDictionary.Count - 1}, or type \"b\" to go back",
                    ConsoleColor.Green);
                var userInput = CustomOutputs.ConsoleReadLine(ConsoleColor.Yellow);

                if (IsNumeric(userInput, out var numericInput)){
                    if (IsValidNumber(ref numericInput)){
                        value = numericInput;
                        return true;
                    }

                    Console.WriteLine("Wrong input, try again ");
                    continue;
                }

                if (IsValidCharacter(ref userInput)){
                    value = "b" + GetPreviousSite(userInput);
                    return false;
                }

                break;
            }

            value = "";
            return default;
        }

        bool IsNumeric(string s, out int userChoice){
            return int.TryParse(s, NumberStyles.Integer, new NumberFormatInfo(), out userChoice);
        }

        bool IsValidNumber(ref int value){
            return value < 0 || value > this.sitesDictionary.Count - 1;
        }

        bool IsValidCharacter(ref string value){
            return value == "b";
        }

        string GetPreviousSite(string value){
            if (value == "b"){
                return this.CurrentSiteIndex <= 1 ? this.sitesQue[0] : this.sitesQue[this.CurrentSiteIndex - 1];
            }

            return "";
        }
    }
}