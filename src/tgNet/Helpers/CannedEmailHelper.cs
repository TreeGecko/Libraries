using System.Collections.Generic;
using System.Text.RegularExpressions;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.Helpers
{
    public static class CannedEmailHelper
    {
        public const string TOKEN_REGEX = @"\[{2}(?<TGText>.+)\]{2}";

        public static TGEmail GetEmail(CannedEmail _cannedEmail, TGSerializedObject _source)
        {
            if (_cannedEmail != null)
            {
                TGEmail email = new TGEmail
                {
                    From = ProcessString(_cannedEmail.From, _source),
                    To = new List<string> {ProcessString(_cannedEmail.To, _source)},
                    ReplyTo = ProcessString(_cannedEmail.ReplyTo, _source),
                    Subject = ProcessString(_cannedEmail.Subject, _source),
                    BodyType = _cannedEmail.BodyType,
                    Body = ProcessString(_cannedEmail.Body, _source)
                };

                return email;
            }

            return null;
        }


        private static string ProcessString(string _item, TGSerializedObject _tgs)
        {
            return Regex.Replace(_item, TOKEN_REGEX, delegate(Match _match) {
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
