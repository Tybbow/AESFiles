using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;

namespace AESfiles
{
    public class AESperso
    {
        public string Password { get; set; }
        private byte[] Key { get; set; }
        private byte[] IV { get; set; }
        public bool Recursive = false;

		Aes AesAlg = new AesCryptoServiceProvider();
        Display myDisplay = new Display();
        Stopwatch Watch = new Stopwatch();

        public AESperso()
        {
            Password = string.Empty;
			AesAlg.KeySize = 256;
			AesAlg.BlockSize = 128;
			AesAlg.Mode = CipherMode.CBC;
			AesAlg.Padding = PaddingMode.Zeros;
        }  

        public AESperso(string password)
        {
            MD5 hashmd5 = MD5.Create();
            Password = GetMd5Hash(hashmd5, password);
            Key = Encoding.UTF8.GetBytes(Password);
            IV = Encoding.UTF8.GetBytes(GetMd5Hash(hashmd5, Password).Substring(0, 16));
			AesAlg.Key = Key;
			AesAlg.IV = IV;
        }

		private string GetMd5Hash(MD5 md5Hash, string input)
		{
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
			StringBuilder sBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
				sBuilder.Append(data[i].ToString("x2"));
			return sBuilder.ToString();
		}


        public string EncryptString(string content)
        {
            byte[] toByte;
            byte[] encrypt;
            string toStr;

            toByte = Encoding.ASCII.GetBytes(content);
            encrypt = this.EncryptAES(toByte);
            toStr = Encoding.UTF8.GetString(encrypt);
            return (toStr);

        }

        public string DecryptString(string content)
        {
            byte[] toByte;
            byte[] encrypt;
            string toStr;

            toByte = Encoding.ASCII.GetBytes(content);
            encrypt = this.DecryptAES(toByte);
            toStr = Encoding.UTF8.GetString(encrypt);
            return (toStr);
        }

        private static byte[] ReturnByte(string filepath)
        {
            byte[] buffer;
            try
            {
                buffer = File.ReadAllBytes(filepath);
            }
            catch (Exception)
            {
                buffer = null;
            }
            return buffer;
        }

        private string[] ReturnFiles(string path)
        {

            if (File.Exists(path))
            {
                string[] files = new string[1];
                files[0] = path;
                return (files);
            }
            if (Directory.Exists(path))
            {
                string[] files;
                files = (this.Recursive) ? Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                                                    : Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
                return (files);
            }
            return (null);
        }

        public void EncryptPath(string path)
        {

            string[] arrayfiles = ReturnFiles(path);
            if (arrayfiles.Length == 0)
            {
                myDisplay.DisplayColor(ConsoleColor.Red, string.Format("Files not found !! Exit\r"));
                return;
            }

            foreach(string file in arrayfiles)
            {
                byte[] fs = ReturnByte(file);
                try
                {
                    File.WriteAllBytes(string.Format("{0}.enc", file), this.EncryptAES(fs));
                    File.Delete(path);
                    myDisplay.DisplayColor(ConsoleColor.Green, string.Format("Encrypt File : {0}", string.Format("{0}.enc", file)));
                }
                catch (Exception ex)
                {
                    myDisplay.DisplayColor(ConsoleColor.Red, string.Format("Echec Encrypt File : {0} - {1}", file, ex.Message));
                }
            }
        }

        public void DecryptPath(string path)
        {
            string[] arrayfiles = ReturnFiles(path);

            if (arrayfiles.Length == 0)
            {
                myDisplay.DisplayColor(ConsoleColor.Red, string.Format("Files not found !! Exit\r"));
                return;
            }
            foreach(string file in arrayfiles)
            {
                byte[] fs = ReturnByte(file);
                try
                {
                    if (file.Substring(file.LastIndexOf('.')).Equals(".enc"))
                    {
                        File.WriteAllBytes(file.Replace(".enc", ""), this.DecryptAES(fs));
                        File.Delete(file);
                        myDisplay.DisplayColor(ConsoleColor.Green, string.Format("Decrypt File : {0}", file));
                    }
                }
                catch (Exception ex)
                {
                    myDisplay.DisplayColor(ConsoleColor.Red, string.Format("Echec Decrypt File : {0} - {1}", file, ex.Message));
                }
            }

        }


        public string TimerStart()
        {
            Watch.Start();
            TimeSpan ts = Watch.Elapsed;
            string elapsedTime = String.Format("{0:00} hour(s), {1:00} minute(s), {2:00} secondes", ts.Hours, ts.Minutes, ts.Seconds);
            return (elapsedTime);

        }

        public string TimerStop()
        {
            Watch.Stop();
            TimeSpan ts = Watch.Elapsed;
            string elapsedTime = String.Format("{0:00} hour(s), {1:00} minute(s), {2:00} secondes", ts.Hours, ts.Minutes, ts.Seconds);
            return (elapsedTime);
        }


        private byte[] EncryptAES(byte[] content)
        {
            byte[] encrypt;

            ICryptoTransform encryptor = AesAlg.CreateEncryptor(AesAlg.Key, AesAlg.IV);
			using (MemoryStream msEncrypt = new MemoryStream())
			{
				using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
				{
					csEncrypt.Write(content, 0, content.Length);
					csEncrypt.FlushFinalBlock();
					encrypt = msEncrypt.ToArray();
				}
			}
            return (encrypt);
		}

        private byte[] DecryptAES(byte[] content)
        {
            byte[] decrypt;

			ICryptoTransform decryptor = AesAlg.CreateDecryptor(AesAlg.Key, AesAlg.IV);
			using (MemoryStream msDecrypt = new MemoryStream())
			{
				using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
				{
					csDecrypt.Write(content, 0, content.Length);
					csDecrypt.FlushFinalBlock();
					decrypt = msDecrypt.ToArray();
				}
			}
            return (decrypt);
        }
    }
}
