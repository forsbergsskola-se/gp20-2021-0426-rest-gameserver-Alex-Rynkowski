using System;
using System.Collections.Generic;
using System.Globalization;

namespace TinyBrowser{
    public class UserInput{
        readonly Dictionary<int, string> sitesDictionary;

        public UserInput(Dictionary<int, string> sitesDictionary){
            this.sitesDictionary = sitesDictionary;
        }

        public T GetUserChoice<T>(){
            int userChoice;
            while (true){
                CustomOutputs.ConsoleWriteLine(
                    $"Type in a number between 0-{this.sitesDictionary.Count - 1}, or type \"b\" to go back",
                    ConsoleColor.Green);
                var userInput = CustomOutputs.ConsoleReadLine(ConsoleColor.Yellow);

                if (IsNumeric(userInput, out var numericInput)){
                    if (IsValidNumber(ref numericInput)){
                        return (T) Convert.ChangeType(numericInput, typeof(int));
                    }

                    Console.WriteLine("Wrong input, try again ");
                    continue;
                }

                if (IsValidCharacter(ref userInput)){
                    return (T) Convert.ChangeType(numericInput, typeof(string));
                }

                break;
            }

            return default;
        }

        bool IsNumeric(string s, out int userChoice){
            return int.TryParse(s, NumberStyles.Integer, new NumberFormatInfo(), out userChoice);
        }

        bool ValidateUserInput(string userInput, out int userChoice){
            var result = int.TryParse(userInput, NumberStyles.Integer, new NumberFormatInfo(), out userChoice);
            return userChoice < 0 || userChoice > this.sitesDictionary.Count - 1 || !result;
        }

        bool IsValidNumber(ref int value){
            return value < 0 || value > this.sitesDictionary.Count - 1;
        }

        bool IsValidCharacter(ref string value){
            return value == "b";
        }
    }
}