using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace TreeGecko.Library.Common.Helpers
{

    public class EmailHelper
    {
        public static bool IsValidEmail(string _emailAddress)
        {
            const string emailPattern = "^[A-Z0-9._%-]+@[A-Z0-9.-]+\\.(?:[A-Z]{2}|com|org|net|biz|info|name|aero|biz|info|jobs|museum|name)$";

            return Regex.IsMatch(_emailAddress, emailPattern, RegexOptions.IgnoreCase);
        }
        public static void SendMessage(MailMessage _mail)
        {
            try
            {

                SmtpClient client = new SmtpClient();
                client.Host = Config.GetSettingValue("SMTP_HOST");

                
                string userName = Config.GetSettingValue("SMTP_USER");
                string password = Config.GetSettingValue("SMTP_PASSWORD");
                string domain = Config.GetSettingValue("SMTP_DOMAIN");

                NetworkCredential credentials = new NetworkCredential(userName, password, domain);
                client.Credentials = credentials;
                try
                {
                    client.Send(_mail);
                    string logMessage = String.Format("Email sent to {0} with subject {1}", _mail.To[0].User, _mail.Subject);
                    TraceFileHelper.Info(logMessage);
                }
                catch (Exception ex)
                {
                    TraceFileHelper.Exception(ex.ToString());

                    string logMessage = String.Format("Unable to send email to {0} with subject {1}", _mail.To[0].User, _mail.Subject);
                    TraceFileHelper.Info(logMessage);
                }

            }
            catch (Exception ex)
            {
                TraceFileHelper.Exception(ex);
            }
        }

        public static void SendMessage(string _from, string _to, 
            string _subject, string _body)
        {
            try
            {
                MailMessage mm = new MailMessage(_from, _to);
                mm.Subject = _subject;
                mm.Body = _body;
                mm.IsBodyHtml = true;

                SendMessage(mm);
            }
            catch (Exception ex)
            {
                TraceFileHelper.Error(ex.ToString());
            }
        }

        public static void ParseList(ref MailMessage _mm, string _addresses)
        {
            string[] theAddresses = _addresses.Split(";".ToCharArray());

            foreach (string address in theAddresses)
            {
                MailAddress ma = new MailAddress(address);

                _mm.To.Add(ma);
            }
        }
    }
}
