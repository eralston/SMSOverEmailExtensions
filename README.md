SMSOverEmailExtensions
======================

A .Net library for sending SMS/Text messages via E-mail

Background
--------------
In the US, cell carriers allow conversion of e-mail to SMS using a fixed mapping of phone number to special e-mail address. This means it's possible to send text messages from your app using e-mail as long as you have the complete phone number and carrier.

Comparison to Original Library
--------------------
SMSOverEmailExtensions is based on the SMSOverEmail library, originally found on Codeplex here:

http://smsoveremail.codeplex.com/

This implementation offers these advantages:

1) Exposes the mapping itself through the CarrierInfo class
2) Ties the mapping to email using just extension methods on System.Net.Mail.MailMessage, offering greater flexibility such as being able to send mail from an SMS address.
3) Comes packaged with some additional extension methods on System.Net.Mail.MailMessage that are very handy
4) The mapping of carriers to names and templates is encapsulated such that it's easier to use with ASP.Net MVC

Finally, it includes a small generator app that can creates code for the carrier-to-email mapping from a CSV file. It was created to simplify re-factoring the definition of the carrier mappings. This has a side-effect of making the data of the library easier to update and maintain.

Installation
--------------------
Download, build, and include in project.

NuGet deploument forthcoming.

Usage
--------------------
To use the library, simply include the library at the top of the file:

```CSharp
using SMSOverEmail;
```

Instantiate System.Net.Mail.MailMessage and start setting your to values:

```CSharp
// Send the message
MailMessage message = new MailMessage();

message.AddToSMS("5555555555555, Carrier.ATT);
message.Subject = "Example Subject";
message.SetBody("Example Body");

message.SendMailSmtp();
```






