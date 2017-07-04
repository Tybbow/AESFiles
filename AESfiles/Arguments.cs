using System;
using System.IO;

namespace AESfiles
{
    public class Arguments
    {

        public string Path { get; set; }
        public string Password { get; set; }
        public string Method { get; set; }
        public int Type { get; private set; }

        public Arguments()
        {
            Password = string.Empty;
        }

        public Arguments(string[] args)
        {
			int i = 0;

			while (i < args.Length - 1)
			{
                if (args[i] == "-f" || args[i] == "--files")
                    this.Path = args[i + 1];
				if (args[i] == "-m" || args[i] == "--method")
					this.Method = args[i + 1];
				i++;
			}
        }

        public void EnterPassword()
        {
            Display MyDisplay = new Display();
            MyDisplay.DisplayColor("Yellow", "Enter your password : ");
            string password = "";
			ConsoleKeyInfo info = Console.ReadKey(true);
			while (info.Key != ConsoleKey.Enter)
			{
				if (info.Key != ConsoleKey.Backspace)
				{
					Console.Write("*");
					password += info.KeyChar;
				}
				else if (info.Key == ConsoleKey.Backspace)
				{
					if (!string.IsNullOrEmpty(password))
					{
						password = password.Substring(0, password.Length - 1);
						int pos = Console.CursorLeft;
						Console.SetCursorPosition(pos - 1, Console.CursorTop);
						Console.Write(" ");
						Console.SetCursorPosition(pos - 1, Console.CursorTop);
					}
				}
				info = Console.ReadKey(true);
			}
			Console.WriteLine();
            Password = password;
        }

        public bool CheckArguments()
        {
            if (!this.CheckMethod())
                return (false);
            if (!this.CheckPath())
                return (false);
            return (true);
        }

        private bool CheckPassword()
        {
            if (!string.IsNullOrEmpty(Password))
                return (true);
            Console.WriteLine("Error Password");
            return (false);
        }

        private bool CheckPath()
        {
            if (File.Exists(this.Path))
            {
                Type = 1;
                return (true);
            }
            if (Directory.Exists(this.Path))
            {
                Type = 2;
                return (true);
            }
            Console.WriteLine("Error Path, File or Directory doesn't exist.");
            return (false);
        }

        private bool CheckMethod()
        {
			if (this.Method == "dec" || this.Method == "enc")
			    return (true);
			Console.WriteLine("Error Method : dec or enc.");
			return (false);
        }

        public string ReadPath()
        {
            return (Path);
        }

        public string ReadPassword()
        {
            return (Password);
        }

        public string ReadMethod()
        {
            return (Method);
        }
    }
}
