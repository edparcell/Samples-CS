using System;

namespace AES
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string cipherText = "U2FsdGVkX19IHNsaQjSutr7BJDWPMWzkAtnsYi3olqDg64l2F5DxSCa4QJB7A4vx";
			string password = "password";
			string plainText = AES.Decipher(cipherText, password);
			Console.WriteLine ("{0} -> {1}", cipherText, plainText);
		}
	}
}
