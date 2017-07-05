using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace AESfiles
{
    public class AESperso
    {
        public string Password { get; set; }
        private byte[] Key { get; set; }
        private byte[] IV { get; set; }
		Aes AesAlg = new AesCryptoServiceProvider();

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

        public byte[] EncryptAES(byte[] content)
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

        public byte[] DecryptAES(byte[] content)
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
