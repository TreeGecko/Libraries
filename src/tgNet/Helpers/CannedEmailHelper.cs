using System.Text.RegularExpressions;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.Helpers
{
    public static class CannedEmailHelper
    {
        public const string TokenRegex = @"\[{2}(?<TGText>.+?)\]{2}";

        public static TGEmail GetEmail(CannedEmail _cannedEmail, TGSerializedObject _source)
        {
            if (_cannedEmail != null)
            {
                TGEmail email = new TGEmail
                {
                    From = ProcessString(_cannedEmail.From, _source),
                    To = ProcessString(_cannedEmail.To, _source),
                    ReplyTo = ProcessString(_cannedEmail.ReplyTo, _source),
                    Subject = ProcessString(_cannedEmail.Subject, _source),
                    BodyType = _cannedEmail.BodyType,
                    Body = ProcessString(_cannedEmail.Body, _source)
                };

                return email;
            }

            return null;
        }

        public static void PopulateEmail(CannedEmail _cannedEmail, 
            TGEmail _email, TGSerializedObject _source)
        {
            if (_cannedEmail != null)
            {
                _email.From = ProcessString(_cannedEmail.From, _source);
                _email.To = ProcessString(_cannedEmail.To, _source);
                _email.ReplyTo = ProcessString(_cannedEmail.ReplyTo, _source);
                _email.Subject = ProcessString(_cannedEmail.Subject, _source);
                _email.BodyType = _cannedEmail.BodyType;
                _email.Body = ProcessString(_cannedEmail.Body, _source);
            }
        }


        private static string ProcessString(string _item, TGSerializedObject _tgs)
        {
            return Regex.Replace(_item, TokenRegex, delegate(Match _match) {
                if (_match.Captures.Count > 0)
                {
                    string property = _match.Groups["TGText"].Captures[0].Value;

                    return _tgs.GetString(property);
                }

                return null;
            });
        }
    }
}
