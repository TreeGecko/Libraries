using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.SimpleEmail;
using TreeGecko.Library.AWS.Helpers;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.Helpers
{
    public static class EmailHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_email"></param>
        public static void SendMessage(TGEmail _email)
        {
            SESHelper.SendMessage(_email.From, _email.To, _email.ReplyTo, _email.Subject, _email.Body);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ses"></param>
        /// <param name="_email"></param>
        public static void SendMessage(AmazonSimpleEmailServiceClient _ses, TGEmail _email)
        {
            List<string> to = new List<string> { _email.To };

            SESHelper.SendMessage(_ses, _email.From, to, _email.ReplyTo, _email.Subject, _email.Body);
        }
    }
}
