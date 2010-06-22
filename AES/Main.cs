using System;

namespace AES
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string originalText = "Hello, secure world!";
			string password = "password";
			string cipherText = AES.Encipher(originalText, password, true);
			//string cipherText = "U2FsdGVkX19IHNsaQjSutr7BJDWPMWzkAtnsYi3olqDg64l2F5DxSCa4QJB7A4vx";

			string plainText = AES.Decipher(cipherText, password);
			Console.WriteLine ("{0} -> {1} -> {2}", originalText, cipherText, plainText);
		}
	}
}
