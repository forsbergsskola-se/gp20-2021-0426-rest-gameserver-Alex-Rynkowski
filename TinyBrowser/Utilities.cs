﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TinyBrowser{
    public static class Utilities{
        public static StringBuilder Builder(string siteToConnectTo, string newLink){
            var builder = new StringBuilder();
            builder.AppendLine($"GET /{newLink} /1.1");
            builder.AppendLine($"Host: {siteToConnectTo}");
            builder.AppendLine("Connection: close");
            builder.AppendLine();
            return builder;
        }
        
        public static string FindTextBetween(string textToSearch, string startText, string endText, ref int startAtIndex){
            var startIndex = textToSearch.IndexOf(startText, startAtIndex, StringComparison.Ordinal) + startText.Length;
            var endIndex = textToSearch.IndexOf(endText, startIndex, StringComparison.Ordinal);

            var findLastIndex = textToSearch.LastIndexOf(startText, endIndex, StringComparison.Ordinal);
            if (startAtIndex > findLastIndex)
                return "";

            startAtIndex = endIndex + 1;
            var newText = textToSearch.Remove(endIndex).Substring(startIndex);

            return newText;
        }

        public static void PrintOutSites(Dictionary<int, string> sites){
            foreach (var (key, value) in sites){
                CustomOutputs.ConsoleWriteLine($"{key} {value.Replace('/', ' ')} ({value})");
            }
        }

        public static bool IsBadRequest(string valueToSearch){
            return valueToSearch.Contains("Bad Request") || valueToSearch.Contains("Not Found");
        }
    }
}