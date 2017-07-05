using System;
namespace AESfiles
{
    public class Display
    {
        public Display()
        {
        }

        public void DisplayColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.Write(" [+] ");
            Console.ResetColor();
            Console.Write("{0}", text);
            if (color != ConsoleColor.Yellow)
                Console.Write("\r\n");
        }

        public void displayHelp()
        {
            Console.WriteLine("Help AESFiles Version: 1.0.0.0 - By Tybbow Copyright");
            Console.WriteLine("");
            Console.WriteLine("Usage : AESfiles [Options....]");
            Console.WriteLine("\t-f, --files : \tTarget file or directory.");
            Console.WriteLine("\t-m, --method : \tenc for encrypt or dec for decrypt.");
            Console.WriteLine("");
            Console.WriteLine("Example :");
			Console.WriteLine("AESfiles.exe -m enc -f myfile");
			Console.WriteLine("AESfiles.exe -m dec -f mydirectory");
        }
    }
}
