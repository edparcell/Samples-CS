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

Convert2Ico
-----------

Sample code to load a .PNG file and save it as a .ICO file, suitable for use in a SystemTray app.

NamedPipeServer/NamedPipeClient
-------------------------------

NamedPipeServer contains sample code to create a named pipe and listen for messages, and print them. NamedPipeClient contains sample code to connect to the named pipe and push a couple of messages. There is also a C++ sample client in the Samples-CXX repository.

Note: These projects does not work on MonoDevelop on Mac.

RabbitMQPub/RabbitMQSub
-----------------------

RabbitMQPub is a simple example of a RabbitMQ publisher that publishes a "Hello world" message once per second. RabbitMQSub is a simple example of a RabbitMQ consumer that listens for messages, printing their contents. When you run these together, RabbitMQSub should print each of the messages sent by RabbitMQPub.

For a swift but useful introduction to the concepts behind messaging in RabbitMQ, I recommend looking at Dmitriy Samovskiy's presentation at http://www.slideshare.net/somic/introduction-to-amqp-messaging-with-rabbitmq

SystemTray
----------

SystemTray is a simple application that runs from the system tray. Right-clicking the tray icon displays a context menu. Left-clicking the tray icon displays the main window. The application does not quit if the main window is closed, instead continuing to run from the system tray.

