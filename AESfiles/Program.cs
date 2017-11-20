using System;
using System.IO;
using System.Diagnostics;

namespace AESfiles
{
    class MainClass
    {

        static Display MyDisplay = new Display();
        static Arguments MyArgs { get; set; }

        public static int Main(string[] args)
        {
            MyArgs =  new Arguments(args);
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
            Start();
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00} hour(s), {1:00} minute(s), {2:00} secondes",ts.Hours, ts.Minutes, ts.Seconds);
            Console.Write("\r\n");
            MyDisplay.DisplayColor(ConsoleColor.Yellow, string.Format("Time Duration : {0}", elapsedTime));
            return (1);
        }

        private static void Start()
        {
            AESperso myAes = new AESperso(MyArgs.ReadPassword());
            if (MyArgs.Type == 1)
                FileOnce(MyArgs.ReadPath(), myAes);
            if (MyArgs.Type == 2)
                MultiFiles(myAes, MyArgs.Recursive);
        }

		private static void MultiFiles(AESperso myAes, bool Recursive)
		{
            var files = (Recursive) ? Directory.EnumerateFiles(MyArgs.ReadPath(), "*.*", SearchOption.AllDirectories) 
                                               : Directory.EnumerateFiles(MyArgs.ReadPath(), "*.*", SearchOption.TopDirectoryOnly);
			foreach (var filepath in files)
			{
                FileOnce(filepath, myAes);
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

        private static void FileOnce(string filepath, AESperso myAes)
        {
            byte[] fs = ReturnByte(filepath);
            if (fs != null)
            {
				if (MyArgs.ReadMethod() == "enc")
				{
					try
					{
                        File.WriteAllBytes(string.Format("{0}.enc", filepath), myAes.EncryptAES(fs));
                        File.Delete(filepath);
                        MyDisplay.DisplayColor(ConsoleColor.Green, string.Format("Encrypt File : {0}", string.Format("{0}.enc", filepath)));
					}
                    catch (Exception ex)
					{
                        MyDisplay.DisplayColor(ConsoleColor.Red, string.Format("Echec Encrypt File : {0} - {1}", filepath, ex.Message));
					}
				}
				else if (MyArgs.ReadMethod() == "dec")
				{
					try
					{
                        if (filepath.Substring(filepath.LastIndexOf('.')).Equals(".enc"))
                        {
                            File.WriteAllBytes(filepath.Substring(0, filepath.LastIndexOf('.')), myAes.DecryptAES(fs));
                            File.Delete(filepath);
                            MyDisplay.DisplayColor(ConsoleColor.Green, string.Format("Decrypt File : {0}", filepath));   
                        }
					}
					catch (Exception ex)
					{
                        MyDisplay.DisplayColor(ConsoleColor.Red, string.Format("Echec Decrypt File : {0} - {1}", filepath, ex.Message));
					}
				}   
            }
        }
    }
}
