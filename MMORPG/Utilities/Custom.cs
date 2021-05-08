using System;
using System.Text;

namespace MMORPG.Utilities{
    public static class Custom{
        public static void WriteLine(string arg, ConsoleColor color){
            Console.WriteLine(arg, Console.ForegroundColor = color);
        }

        public static void WriteLineResetColor(string arg){
            Console.ResetColor();
            Console.WriteLine(arg);
        }

        public static string ReadLine(ConsoleColor color){
            Console.ForegroundColor = color;
            return Console.ReadLine();
        }

        public static void MultiWriteLine(ConsoleColor color, params string[] args){
            Console.ForegroundColor = color;
            foreach (var t in args){
                Console.WriteLine(t);
            }
        }
    }
}