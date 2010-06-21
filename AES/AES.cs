
using System;

namespace AES
{
	public class AES
	{
		private static byte[] salted_string = Encoding.UTF8.GetBytes("Salted__");
		private static MD5 md5 = MD5.Create();

		public static void Decipher(byte[] Data, byte[] key, byte[] iv)
        {

			RijndaelManaged rijndael = new RijndaelManaged();
			rijndael.Mode = CipherMode.CBC;
			rijndael.Padding = PaddingMode.PKCS7;
			rijndael.KeySize = 128;
			rijndael.BlockSize = 128;
			rijndael.Key = key;
			rijndael.IV = iv;
			
			ICryptoTransform rijndaelDecryptor = rijndael.CreateDecryptor();
			
			MemoryStream msDecrypt = new MemoryStream(enc_data);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, rijndaelDecryptor, CryptoStreamMode.Read);
            StreamReader srDecrypt = new StreamReader(csDecrypt);
			
			string clearText = srDecrypt.ReadToEnd();
			return clearText;
		}

		public static string Decipher(string Data, string Password)
        {
			byte[] enc_data = Convert.FromBase64String(Data);
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
				
			try
			{
				byte[] preKey = new byte[password.Length + salt.Length];
				Buffer.BlockCopy(password, 0, preKey, 0, password.Length);
				Buffer.BlockCopy(salt, 0, preKey, password.Length, salt.Length);
				key = md5.ComputeHash(preKey);				
				
				byte[] preIV = new byte[key.Length + preKey.Length];				
				Buffer.BlockCopy(key, 0, preIV, 0, key.Length);
				Buffer.BlockCopy(preKey, 0, preIV, key.Length, preKey.Length);
				iv = md5.ComputeHash(preIV);
			}
			finally
			{
				md5.Clear();
			}
			
			return Decipher(enc_data, key, iv);
        }

		private static string ToHexString (byte[] b)
		{
			StringBuilder sb = new StringBuilder();
			for (int i=0; i<b.Length; ++i) {
				sb.Append(b[i].ToString("X"));
			}
			return sb.ToString();
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
