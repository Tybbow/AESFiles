using System;
using System.IO;
using System.Diagnostics;

namespace AESfiles
{
    class MainClass
    {

        static Display MyDisplay = new Display();

        public static int Main(string[] args)
        {
            Arguments myArgs = new Arguments(args);
            if (args.Length == 0 || !myArgs.CheckArguments() || myArgs.Help)
            {
                MyDisplay.displayHelp();
                return (0);
            }
			MyDisplay.displayBegin();
            myArgs.EnterPassword();
            if (string.IsNullOrEmpty(myArgs.ReadPassword()))
            {
                Console.WriteLine("Error Password, enter your password ! No Empty use");
                return (0);
            }
            Start(myArgs);
            return (1);
        }

        private static void Start(Arguments myArgs)
        {
            
            AESperso myAes = new AESperso(myArgs.ReadPassword());
            myAes.TimerStart();
            if (!string.IsNullOrEmpty(myArgs.String))
            {
                if (myArgs.Method == "enc")
                    Console.WriteLine(myAes.EncryptString(myArgs.String));
                else
                    Console.WriteLine(myAes.DecryptString(myArgs.String));
            }
            if (!string.IsNullOrEmpty(myArgs.Path))
            {
                myAes.Recursive = myArgs.Recursive;
                if (myArgs.Method == "enc")
                    myAes.EncryptPath(myArgs.Path);
                else
                    myAes.DecryptPath(myArgs.Path);
            }
            myAes.TimerStop();
        }
    }
}
