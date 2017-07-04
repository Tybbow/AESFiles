using System;
namespace AESfiles
{
    public class Display
    {
        public Display()
        {
        }

        public void DisplayColor(string color, string text)
        {
            if (color == "Yellow")
            {
                DisplayYellow();
                Console.Write(string.Format("{0}", text));
            }
            if (color == "Green")
            {
                DisplayGreen();
                Console.Write(string.Format("{0}\r\n", text));
            }
            if (color == "Red")
            {
                DisplayRed();
                Console.Write(string.Format("{0}\r\n", text));
            }
        }

        private void DisplayYellow()
        {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("[+] ");
			Console.ResetColor();
        }

		private void DisplayGreen()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("[+] ");
			Console.ResetColor();
		}

		private void DisplayRed()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("[+] ");
			Console.ResetColor();
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
