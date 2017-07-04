using System;
using System.IO;

namespace AESfiles
{
    class MainClass
    {
        public static int Main(string[] args)
        {
            Arguments MyArgs = new Arguments(args);
            if (!MyArgs.CheckArguments())
            {
                Console.WriteLine("Application Exit");
                return (0);
            }
            MyArgs.EnterPassword();
            Console.WriteLine();
            if (string.IsNullOrEmpty(MyArgs.ReadPassword()))
            {
                Console.WriteLine("Error Password, enter your password ! No Empty use");
                Console.WriteLine("Application Exit");
                return (0);
            }
            Start(MyArgs);
            return (1);
        }

        private static void Start(Arguments MyArgs)
        {

            AESperso myAes = new AESperso(MyArgs.ReadPassword());

            if (MyArgs.Type == 1)
                FileOnce(MyArgs.ReadPath(), MyArgs.ReadMethod(), myAes);
            if (MyArgs.Type == 2)
                MultiFiles(MyArgs.ReadPath(), MyArgs.ReadMethod(), myAes);
        }

        private static void FileOnce(string filepath, string method, AESperso myAes)
        {

            Display dis = new Display();

            byte[] fs = File.ReadAllBytes(filepath);
			if (method == "enc")
            {
                try
                {
                    File.WriteAllBytes(filepath, myAes.EncryptAES(fs));
					dis.DisplayColor("Green", string.Format("Encrypt File : {0}", filepath));
                }
                catch
                {
                    dis.DisplayColor("Red", string.Format("Echec Encrypt File : {0}", filepath));
                }
            }
			else if (method == "dec")
			{
				try
				{
					File.WriteAllBytes(filepath, myAes.DecryptAES(fs));
                    dis.DisplayColor("Green", string.Format("Decrypt File : {0}", filepath));
				}
				catch
				{
					dis.DisplayColor("Red", string.Format("Echec Decrypt File : {0}", filepath));
				}
			}
        }

        private static void MultiFiles(string directory, string method, AESperso myAes)
        {
            Display dis = new Display();
            var files = Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories);
            foreach (var curentfile in files)
            {
                byte[] fs = File.ReadAllBytes(curentfile);
				if (method == "enc")
				{
					try
					{
						File.WriteAllBytes(curentfile, myAes.EncryptAES(fs));
						dis.DisplayColor("Green", string.Format("Encrypt File : {0}", curentfile));
					}
					catch
					{
						dis.DisplayColor("Red", string.Format("Echec Encrypt File : {0}", curentfile));
					}
				}
				else if (method == "dec")
				{
					try
					{
						File.WriteAllBytes(curentfile, myAes.DecryptAES(fs));
						dis.DisplayColor("Green", string.Format("Decrypt File : {0}", curentfile));
					}
					catch
					{
						dis.DisplayColor("Red", string.Format("Echec Decrypt File : {0}", curentfile));
					}
				}
            }
        }
    }
}
