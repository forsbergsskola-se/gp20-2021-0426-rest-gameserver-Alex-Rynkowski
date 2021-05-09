using System;

namespace Client.Utilities{
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

        public static void WriteMultiLines(ConsoleColor textColor, params string[] args){
            Console.ForegroundColor = textColor;
            foreach (var arg in args){
                Console.WriteLine(arg);
            }
        }

        /// <summary>
        /// If value = "Quit" then application stops running.
        /// </summary>
        /// <param name="value"></param>
        public static void Exit(string value){
            if (value.ToLower() == "quit")
                Environment.Exit(0);
        }
    }
}