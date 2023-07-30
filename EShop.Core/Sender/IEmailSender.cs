namespace EShop.Core.Sender
{
    public interface IEmailSender
    {
        void SendEmail(string to, string subject, string body);
    }
}
