
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AES
{
	public class AES
	{
		private static byte[] salted_string = Encoding.UTF8.GetBytes("Salted__");
		
		public static byte[] Encipher(string ClearText, byte[] key, byte[] iv)
		{
			RijndaelManaged rijndael = MakeRijndael(key, iv);
			
			ICryptoTransform rijndaelEncryptor = rijndael.CreateEncryptor();
			
			MemoryStream msEncrypt = new MemoryStream();
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, rijndaelEncryptor, CryptoStreamMode.Write))
			using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
			{
				swEncrypt.Write(ClearText);
			}
			return msEncrypt.ToArray();
		}
		
		public static string Encipher(string ClearText, string Password, bool Salted)
        {
			byte[] password = Encoding.UTF8.GetBytes(Password);				
			byte[] salt = new byte[0];
			if (Salted) 
			{
				salt = new byte[8];
				RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
				rng.GetBytes(salt);
			}
			
			byte[] key = null;
			byte[] iv = null;
			GetKeyAndIV (password, salt, out key, out iv);
			
			byte[] CipherText = Encipher(ClearText, key, iv);
			if (Salted)
			{
				byte[] CipherTextWithSalt = CopyBlocks(salted_string, salt, CipherText);
				CipherText = CipherTextWithSalt;
			}
			return Convert.ToBase64String(CipherText);
		}

		public static string Decipher(byte[] CipherData, byte[] Key, byte[] IV)
        {
			RijndaelManaged rijndael = MakeRijndael(Key, IV);
			
			ICryptoTransform rijndaelDecryptor = rijndael.CreateDecryptor();
			
			MemoryStream msDecrypt = new MemoryStream(CipherData);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, rijndaelDecryptor, CryptoStreamMode.Read);
            StreamReader srDecrypt = new StreamReader(csDecrypt);
			
			string clearText = srDecrypt.ReadToEnd();
			return clearText;
		}

		public static string Decipher(string CipherData, string Password)
        {
			byte[] enc_data = Convert.FromBase64String(CipherData);
			byte[] password = Encoding.UTF8.GetBytes(Password);				
			
			byte[] salt = new byte[0];
			bool isSalted = (enc_data.Length > 16 && IsDataEqual(enc_data, 0, salted_string, 8));
			
			if (isSalted) {
				salt = new byte[8];
				Buffer.BlockCopy(enc_data, 8, salt, 0, 8);
				int dataLength = enc_data.Length - 16;
				byte[] tmp = new byte[dataLength];
				Buffer.BlockCopy(enc_data, 16, tmp, 0, dataLength);
				enc_data = tmp;
			}
			
			byte[] key = null;
			byte[] iv = null;				
			GetKeyAndIV (password, salt, out key, out iv);
			
			return Decipher(enc_data, key, iv);
        }
		
		private static void GetKeyAndIV (byte[] password, byte[] salt, out byte[] key, out byte[] iv)
		{
			MD5 md5 = MD5.Create();
			try
			{
				byte[] preKey = CopyBlocks(password, salt);
				key = md5.ComputeHash(preKey);				
				
				byte[] preIV = CopyBlocks(key, preKey);
				iv = md5.ComputeHash(preIV);
			}
			finally
			{
				md5.Clear();
			}
		}

		private static RijndaelManaged MakeRijndael (byte[] Key, byte[] IV)
		{
			RijndaelManaged rijndael = new RijndaelManaged();
			rijndael.Mode = CipherMode.CBC;
			rijndael.Padding = PaddingMode.PKCS7;
			rijndael.KeySize = 128;
			rijndael.BlockSize = 128;
			rijndael.Key = Key;
			rijndael.IV = IV;
			return rijndael;
		}

		private static string ToHexString (byte[] b)
		{
			StringBuilder sb = new StringBuilder();
			for (int i=0; i<b.Length; ++i) {
				sb.Append(b[i].ToString("X"));
			}
			return sb.ToString();
		}

		private static byte[] CopyBlocks(params byte[][] blocks)
		{
			int length=0;
			foreach (byte[] block in blocks) length += block.Length;
			byte[] result = new byte[length];
			int offset=0;
			foreach (byte[] block in blocks)
			{
				Buffer.BlockCopy(block, 0, result, offset, block.Length);
				offset += block.Length;
			}
			return result;
		}

		private static bool IsDataEqual (byte[] enc_data, int offset, byte[] salted_string, int length)
		{
			for (int i=0; i<length; ++i) {
				if (enc_data[offset + i] != salted_string[i]) return false;
			}
			return true;
		}

	}
}
