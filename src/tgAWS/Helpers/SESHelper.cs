using System.Collections.Generic;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using TreeGecko.Library.Common.Helpers;


namespace TreeGecko.Library.AWS.Helpers
{
    public static class SESHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_from"></param>
        /// <param name="_to"></param>
        /// <param name="_replyTo"></param>
        /// <param name="_subject"></param>
        /// <param name="_bodyHtml"></param>
        /// <returns></returns>
        public static Message GetMessage(string _from,
                                         List<string> _to,
                                         string _replyTo,
                                         string _subject,
                                         string _bodyHtml)
        {
            Content subject = new Content(_subject);

            Body body = new Body {Html = new Content(_bodyHtml)};

            Message message = new Message(subject, body);

            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ses"></param>
        /// <param name="_from"></param>
        /// <param name="_to"></param>
        /// <param name="_replyTo"></param>
        /// <param name="_subject"></param>
        /// <param name="_body"></param>
        public static void SendMessage(AmazonSimpleEmailServiceClient _ses,
                                       string _from,
                                       List<string> _to,
                                       string _replyTo,
                                       string _subject,
                                       string _body)
        {
            Message message = GetMessage(_from, _to, _replyTo, _subject, _body);

            Destination destination = new Destination(_to);
            SendEmailRequest request = new SendEmailRequest(_from, destination, message);
            
            SendEmailResponse response = _ses.SendEmail(request);
           
            if (response != null)
            {
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_from"></param>
        /// <param name="_to"></param>
        /// <param name="_replyTo"></param>
        /// <param name="_subject"></param>
        /// <param name="_body"></param>
        public static void SendMessage(string _from,
                                       string _to,
                                       string _replyTo,           
                                       string _subject,
                                       string _body)
        {
            SendMessage(_from, new List<string> { _to }, _replyTo, _subject, _body);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_from"></param>
        /// <param name="_to"></param>
        /// <param name="_replyTo"></param>
        /// <param name="_subject"></param>
        /// <param name="_body"></param>
        public static void SendMessage(string _from,
                                       List<string> _to,
                                       string _replyTo,
                                       string _subject,
                                       string _body)
        {
            using (AmazonSimpleEmailServiceClient ses = GetSES())
            {
                SendMessage(ses, _from, _to, _replyTo, _subject, _body);
            }

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
        /// <param name="_accessKey"></param>
        /// <param name="_secretKey"></param>
        /// <returns></returns>
        public static AmazonSimpleEmailServiceClient GetSES(string _accessKey, string _secretKey)
        {
            if (_accessKey != null
                && _secretKey != null)
            {
                return new AmazonSimpleEmailServiceClient(_accessKey, _secretKey, RegionEndpoint.USEast1);
            }

            return new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast1);
        }
    }
}
