using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Kavenegar;
using Kavenegar.Core.Models;
using Kavenegar.Core.Models.Enums;

namespace EShop.Core.Sender
{
    public class MessageSender : IEmailSender,ISmsSender
    {
        public void SendEmail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("EmilAddress", "Shop Info");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("EmailAddress", "Password");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        public async Task<bool>  SendSms(string to, string template, params string[] param)
        {


            try
            {
                KavenegarApi kavenegar = new KavenegarApi("496F657A467A6845535859765A45426138667139562B74564B61496677544E5779794B714D2B443161644D3D");

                SendResult result = null;




                #region VerifyLookupAsync

                switch (param.Length)
                {
                    case 1:
                        result = await kavenegar.VerifyLookup(to, param[0], template);
                        break;
                    case 2:
                        result = await kavenegar.VerifyLookup(to, param[0], param[1], null, null, null, template, VerifyLookupType.Sms);
                        break;
                    case 3:
                        result = await kavenegar.VerifyLookup(to, param[0], param[1], param[2], null, null, template, VerifyLookupType.Sms);

                        break;
                    case 4:
                        result = await kavenegar.VerifyLookup(to, param[0], param[1], param[2], param[3], null, template, VerifyLookupType.Sms);
                        break;
                    case 5:
                        result = await kavenegar.VerifyLookup(to, param[0], param[1], param[2], param[3], param[4], template, VerifyLookupType.Sms);
                        break;
                }




                #endregion
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

           

        }

        public bool SendSms(string to, string message)
        {
            try
            {
                KavenegarApi kavenegar = new KavenegarApi("496F657A467A6845535859765A45426138667139562B74564B61496677544E5779794B714D2B443161644D3D");
                var result = kavenegar.Send("10008663", to, message);
                 
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
