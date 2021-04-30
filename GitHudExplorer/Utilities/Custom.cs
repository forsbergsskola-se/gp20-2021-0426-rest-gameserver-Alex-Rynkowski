using System;

namespace GitHudExplorer.Utilities{
    public static class Custom{
        public static void WriteLine(string txt, ConsoleColor textColor){
            Console.WriteLine(txt, Console.ForegroundColor = textColor);
        }

        public static void WriteLineResetColor(string txt){
            Console.ResetColor();
            Console.WriteLine(txt);
        }

        public static string ReadLine(ConsoleColor textColor){
            Console.ForegroundColor = textColor;
            return Console.ReadLine();
        }
    }
}