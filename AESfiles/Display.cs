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
                DisplayGreen();
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

		private static void DisplayGreen()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("[+] ");
			Console.ResetColor();
		}

		private static void DisplayRed()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("[+] ");
			Console.ResetColor();
		}


    }
}
