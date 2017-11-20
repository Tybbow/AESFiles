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
        public bool Help { get; private set; }
        public bool Recursive { get; private set; }
        Display MyDisplay = new Display();

        public Arguments()
        {
            Password = string.Empty;
            Help = false;
            Recursive = false;
            Type = 0;
        }

        public Arguments(string[] args)
        {
			int i = 0;

			while (i < args.Length)
			{
                if (args[i] == "-m" || args[i] == "--method")
                    this.Method = (i < args.Length - 1) ? args[i + 1] : null;
                else if (args[i] == "-h" || args[i] == "--help")
                    this.Help = true;
                else if (args[i] == "-r" || args[i] == "--recursive")
                    this.Recursive = true;
                else
                    this.Path = args[i];
				i++;
			}
        }

        public void EnterPassword()
        {
            MyDisplay.DisplayColor(ConsoleColor.Yellow, "Enter your password : ");
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
            if (this.Help)
                return (false);
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
            MyDisplay.DisplayColor(ConsoleColor.DarkRed, "Error password");
            return (false);
        }

        private bool CheckPath()
        {
            if (File.Exists(this.Path))
                Type = 1;
            if (Directory.Exists(this.Path))
                Type = 2;
            if (Type != 0)
                return (true);
            MyDisplay.DisplayColor(ConsoleColor.DarkRed, "Error Path, File or Directory doesn't exists");
            return (false);
        }

        private bool CheckMethod()
        {
			if (this.Method == "dec" || this.Method == "enc")
			    return (true);
            MyDisplay.DisplayColor(ConsoleColor.DarkRed, "Error Method : enc (Encrypt) or dec (Decrypt).");
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
