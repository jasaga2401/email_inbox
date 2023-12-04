using System;
using System.Net.Mail;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;

class Program
{
    static void Main()
    {
        string email = "<username>";
        string password = "<usual password>";

        using (var client = new ImapClient())
        {
            client.Connect("imap.gmail.com", 993, true);
            client.Authenticate(email, password);

            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            var uids = inbox.Search(SearchQuery.All);

            foreach (var uid in uids)
            {
                var message = inbox.GetMessage(uid);
                Console.WriteLine($"Subject: {message.Subject}");
                Console.WriteLine($"From: {message.From}");
                Console.WriteLine($"Body: {message.TextBody}");
                Console.WriteLine();
            }

            client.Disconnect(true);
        }
    }
}
