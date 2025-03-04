using MailKit.Net.Smtp;
using MimeKit;

namespace SendMail
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello, World!");
    }

    static async Task<string> SendMail()
    {
      MimeMessage msg = new();
      msg.From.Add( new MailboxAddress("test", "test@test.com"));
      msg.To.Add(new MailboxAddress("中川健司", "ddk754@outlook.jp"));

      msg.Subject = "これはテストです。";
      msg.Body = new TextPart("plain") { Text = "これはテストです。" };

      using SmtpClient client = new();
      await client.ConnectAsync("smtp.office365.com", 587);
      await client.AuthenticateAsync("user", "pass");
      string response = await client.SendAsync(msg);
      await client.DisconnectAsync(true);

      return response;
    }
  }
}
