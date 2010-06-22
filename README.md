Samples-CS
==========

This repository contains example code for various using various libraries. The code is licensed under the MIT license (See the file LICENSE for details).

AES
---

Sample code to encrypt and decrypt a string using the AES libraries in C#. This code is structured to be compatible with openssl.

This code is based on information found at the following sources:

* [Deusty: Decrypting OpenSSL AES files in C#](http://deusty.blogspot.com/2009/04/decrypting-openssl-aes-files-in-c.html)
* [Stack Overflow: C# Implementations of AES encryption](http://stackoverflow.com/questions/273452/c-implementations-of-aes-encryption)

Example openssl commands to encrypt, decrypt text:

	$ echo 'Hello, secure world!' | openssl enc -aes-128-cbc -p -a -salt -pass pass:password
	salt=D142523D9DAF06BE
	key=1F4753FB1AE14728748FF8CA10A4A243
	iv =B16138AF9773EFE1EAC5BD8C0639CC74
	U2FsdGVkX1/RQlI9na8Gvt+PB5mQqp7/+JaB3bMFQX9g/5gqacg8WAFnblKNxKdj
	$ echo 'U2FsdGVkX1/RQlI9na8Gvt+PB5mQqp7/+JaB3bMFQX9g/5gqacg8WAFnblKNxKdj' | openssl enc -d -aes-128-cbc -p -a -salt -pass pass:password
	salt=D142523D9DAF06BE
	key=1F4753FB1AE14728748FF8CA10A4A243
	iv =B16138AF9773EFE1EAC5BD8C0639CC74
	Hello, secure world!

