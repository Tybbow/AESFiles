using System;
using System.IO;
using System.Diagnostics;

namespace AESfiles
{
    class MainClass
    {
        public static int Main(string[] args)
        {
            Arguments MyArgs = new Arguments(args);
            Display myDis = new Display();
            Stopwatch stopWatch = new Stopwatch();

            if (!MyArgs.CheckArguments())
            {
                myDis.displayHelp();
                return (0);
            }
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
            string elapsedTime = String.Format("{0:00} minute(s), {1:00} secondes", ts.Minutes, ts.Seconds);
            myDis.DisplayColor(ConsoleColor.Yellow, string.Format("Time Duration : {0}", elapsedTime));
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

		private static void MultiFiles(string directory, string method, AESperso myAes)
		{
			Display dis = new Display();
			var files = Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories);
			foreach (var filepath in files)
			{
                FileOnce(filepath, method, myAes);
			}
		}

		private static byte[] ReturnByte(string filepath)
		{
			Display dis = new Display();
            byte[] buffer;
            try
            {
                buffer = File.ReadAllBytes(filepath);
            }
            catch (Exception ex)
            {
                buffer = null;
                dis.DisplayColor(ConsoleColor.Red, string.Format("{0} - {1}", filepath, ex.Message));
            }
            return buffer;
		}

        private static void FileOnce(string filepath, string method, AESperso myAes)
        {
            Display dis = new Display();
            byte[] fs = ReturnByte(filepath);
            if (fs != null)
            {
				if (method == "enc")
				{
					try
					{
						File.WriteAllBytes(filepath, myAes.EncryptAES(fs));
						dis.DisplayColor(ConsoleColor.Green, string.Format("Encrypt File : {0}", filepath));
					}
                    catch
					{
						dis.DisplayColor(ConsoleColor.Red, string.Format("Echec Encrypt File : {0}", filepath));
					}
				}
				else if (method == "dec")
				{
					try
					{
						File.WriteAllBytes(filepath, myAes.DecryptAES(fs));
						dis.DisplayColor(ConsoleColor.Green, string.Format("Decrypt File : {0}", filepath));
					}
					catch
					{
						dis.DisplayColor(ConsoleColor.Red, string.Format("Echec Decrypt File : {0}", filepath));
					}
				}   
            }
        }
    }
}
