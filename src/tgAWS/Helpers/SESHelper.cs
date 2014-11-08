using System.Collections.Generic;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Net.Enums;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.AWS.Helpers
{
    public static class SESHelper
    {
        public static Message GetMessage(string from,
                                         List<string> to,
                                         string replyTo,
                                         string sSubject,
                                         string sBodyHtml,
                                         EmailBodyType bodyType)
        {
            Content subject = new Content(sSubject);


            Body body = new Body {Html = new Content(sBodyHtml)};

            Message message = new Message(subject, body);

            return message;
        }

        public static void SendMessage(AmazonSimpleEmailServiceClient _ses,
                                       string from,
                                       List<string> to,
                                       string replyTo,
                                       string sSubject,
                                       string sBody,
                                       EmailBodyType bodyType)
        {
            Message message = GetMessage(from, to, replyTo, sSubject, sBody, bodyType);

            Destination destination = new Destination(to);
            SendEmailRequest request = new SendEmailRequest(from, destination, message);
            
            SendEmailResponse response = _ses.SendEmail(request);
           
            if (response != null)
            {
                SendEmailResult result = response.SendEmailResult;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="replyTo"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="bodyType"></param>
        public static void SendMessage(string from,
                                       string to,
                                       string replyTo,           
                                       string subject,
                                       string body,
                                       EmailBodyType bodyType)
        {
            SendMessage(from, new List<string> { to }, replyTo, subject, body, bodyType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="replyTo"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="bodyType"></param>
        public static void SendMessage(string from,
                                       List<string> to,
                                       string replyTo,
                                       string subject,
                                       string body,
                                       EmailBodyType bodyType)
        {
            using (AmazonSimpleEmailServiceClient ses = GetSES())
            {
                SendMessage(ses, from, to, replyTo, subject, body, bodyType);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_email"></param>
        public static void SendMessage(TGEmail _email)
        {
            SendMessage(_email.From, _email.To, _email.ReplyTo, _email.Subject, _email.Body, _email.BodyType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ses"></param>
        /// <param name="_email"></param>
        public static void SendMessage(AmazonSimpleEmailServiceClient _ses, TGEmail _email)
        {
            SendMessage(_ses, _email.From, _email.To, _email.ReplyTo, _email.Subject, _email.Body, _email.BodyType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AmazonSimpleEmailServiceClient GetSES()
        {
            string user = Config.GetSettingValue("AWSAccessKey");
            string password = Config.GetSettingValue("AWSSecretKey");

            return GetSES(user, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessKey"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static AmazonSimpleEmailServiceClient GetSES(string accessKey, string secretKey)
        {
            return new AmazonSimpleEmailServiceClient(accessKey, secretKey);
        }
    }
}
