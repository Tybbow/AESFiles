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
            Console.WriteLine("Help AESFiles Version: 1.4.1 - By Tybbow Copyright");
            Console.WriteLine("");
            Console.WriteLine("Usage : AESfiles [Options....] File or Directory");
            Console.WriteLine("\t-m, --method : \tenc for encrypt or dec for decrypt.");
            Console.WriteLine("\t-s, --string : \ttarget string.");
			Console.WriteLine("\t-f, --files : \ttartget files");
			Console.WriteLine("\t-r, --recursive : \tselect all directory or not. Target directory for use this options");
			Console.WriteLine("\t-h, --help : \tDisplay this help.");
            Console.WriteLine("");
            Console.WriteLine("Example :");
			Console.WriteLine("AESfiles.exe -m enc -f myfile");
            Console.WriteLine("AESfiles.exe -m dec -f mydirectory");
            Console.WriteLine("AESfiles.exe -m enc -f mydirectory -r");
			Console.WriteLine("AESfiles.exe -m enc -s \"My New Target String\"");
        }

        public void displayBegin()
        {
			Console.WriteLine("         ___       _______     _______. _______  __   __       _______     _______.");
			Console.WriteLine("        /   \\     |   ____|   /       ||   ____||  | |  |     |   ____|   /       |");
			Console.WriteLine("       /  ^  \\    |  |__     |   (----`|  |__   |  | |  |     |  |__     |   (----`");
			Console.WriteLine("      /  /_\\  \\   |   __|     \\   \\    |   __|  |  | |  |     |   __|     \\   \\    ");
			Console.WriteLine("     /  _____  \\  |  |____.----)   |   |  |     |  | |  `----.|  |____.----)   |   ");
			Console.WriteLine("    /__/     \\__\\ |_______|_______/    |__|     |__| |_______||_______|_______/    ");
            Console.WriteLine("    By Tybbow, V1.4.1");
			Console.WriteLine();
			Console.WriteLine();

		}
    }
}
