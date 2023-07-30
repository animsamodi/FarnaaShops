using System.Threading.Tasks;

namespace EShop.Core.Sender
{
   public interface ISmsSender
    {
        Task<bool> SendSms(string to, string code, params string[] param);
        bool SendSms(string to, string message);
    }
}
