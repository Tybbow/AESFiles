using System;
using System.IO;
using System.Diagnostics;

namespace AESfiles
{
    class MainClass
    {

        private static Display MyDisplay = new Display();

        public static int Main(string[] args)
        {
            Arguments MyArgs = new Arguments(args);
            Stopwatch stopWatch = new Stopwatch();

            if (args.Length == 0 || !MyArgs.CheckArguments() || MyArgs.Help)
            {
                MyDisplay.displayHelp();
                return (0);
            }
			MyDisplay.displayBegin();
            MyArgs.EnterPassword();
            if (string.IsNullOrEmpty(MyArgs.ReadPassword()))
            {
                Console.WriteLine("Error Password, enter your password ! No Empty use");
                return (0);
            }
            stopWatch.Start();
            Start(MyArgs);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{2:00} hour(s), {0:00} minute(s), {1:00} secondes",ts.Hours, ts.Minutes, ts.Seconds);
            Console.Write("\r\n");
            MyDisplay.DisplayColor(ConsoleColor.Yellow, string.Format("Time Duration : {0}", elapsedTime));
            return (1);
        }

        private static void Start(Arguments MyArgs)
        {
            AESperso myAes = new AESperso(MyArgs.ReadPassword());
            if (MyArgs.Type == 1)
                FileOnce(MyArgs.ReadPath(), MyArgs.ReadMethod(), myAes);
            if (MyArgs.Type == 2)
                MultiFiles(MyArgs.ReadPath(), MyArgs.ReadMethod(), myAes, MyArgs.Recursive);
        }

		private static void MultiFiles(string directory, string method, AESperso myAes, bool Recursive)
		{
            var files = (Recursive) ? Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories) 
                                               : Directory.EnumerateFiles(directory, "*.*", SearchOption.TopDirectoryOnly);
			foreach (var filepath in files)
			{
                FileOnce(filepath, method, myAes);
			}
		}

		private static byte[] ReturnByte(string filepath)
		{
            byte[] buffer;
            try
            {
                buffer = File.ReadAllBytes(filepath);
            }
            catch (Exception ex)
            {
                buffer = null;
                MyDisplay.DisplayColor(ConsoleColor.Red, string.Format("{0} - {1}", filepath, ex.Message));
            }
            return buffer;
		}

        private static void FileOnce(string filepath, string method, AESperso myAes)
        {
            byte[] fs = ReturnByte(filepath);
            if (fs != null)
            {
				if (method == "enc")
				{
					try
					{
						File.WriteAllBytes(filepath, myAes.EncryptAES(fs));
						MyDisplay.DisplayColor(ConsoleColor.Green, string.Format("Encrypt File : {0}", filepath));
					}
                    catch
					{
						MyDisplay.DisplayColor(ConsoleColor.Red, string.Format("Echec Encrypt File : {0}", filepath));
					}
				}
				else if (method == "dec")
				{
					try
					{
						File.WriteAllBytes(filepath, myAes.DecryptAES(fs));
						MyDisplay.DisplayColor(ConsoleColor.Green, string.Format("Decrypt File : {0}", filepath));
					}
					catch
					{
						MyDisplay.DisplayColor(ConsoleColor.Red, string.Format("Echec Decrypt File : {0}", filepath));
					}
				}   
            }
        }
    }
}
