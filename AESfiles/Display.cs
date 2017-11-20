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
            Console.Write("{0} {1}", text, color != ConsoleColor.Yellow ? "\r\n" : "");
        }

        public void displayHelp()
        {
            Console.WriteLine("Help AESFiles Version: 1.1.0 - By Tybbow Copyright");
            Console.WriteLine("");
            Console.WriteLine("Usage : AESfiles [Options....] File or Directory");
			Console.WriteLine("\t-m, --method : \tenc for encrypt or dec for decrypt.");
			Console.WriteLine("\t-r, --recursive : \tselect all directory or not. Target directory for use this options");
			Console.WriteLine("\t-h, --help : \tDisplay this help.");
            Console.WriteLine("");
            Console.WriteLine("Example :");
			Console.WriteLine("AESfiles.exe -m enc myfile");
			Console.WriteLine("AESfiles.exe -m dec mydirectory");
			Console.WriteLine("AESfiles.exe -m enc -r mydirectory");
        }

        public void displayBegin()
        {
			Console.WriteLine("         ___       _______     _______. _______  __   __       _______     _______.");
			Console.WriteLine("        /   \\     |   ____|   /       ||   ____||  | |  |     |   ____|   /       |");
			Console.WriteLine("       /  ^  \\    |  |__     |   (----`|  |__   |  | |  |     |  |__     |   (----`");
			Console.WriteLine("      /  /_\\  \\   |   __|     \\   \\    |   __|  |  | |  |     |   __|     \\   \\    ");
			Console.WriteLine("     /  _____  \\  |  |____.----)   |   |  |     |  | |  `----.|  |____.----)   |   ");
			Console.WriteLine("    /__/     \\__\\ |_______|_______/    |__|     |__| |_______||_______|_______/    ");
            Console.WriteLine("    By Tybbow, V1.1.0");
			Console.WriteLine();
			Console.WriteLine();

		}
    }
}
