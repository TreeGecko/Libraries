using System;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.Interfaces
{
    public interface IServerDataManager
    {
        TGUser GetUser(Guid _userGuid);
        bool ValidateUser(TGUser _user, string _password);

        TGUserAuthorization GetUserAuthorization(Guid _userGuid, string _authorizationToken);
        void Persist(TGUserAuthorization _tgUserAuthorization);
        bool ValidateAuthorizationToken(Guid _userGuid, string _authorizationToken);

        TGUserPassword GetTGUserPassword(Guid _guid);
        TGUserPassword GetUserPasswordByUser(Guid _userGuid);
        void Persist(TGUserPassword _userPassword);

        TGUserEmailValidation GetTGUserEmailValidation(string _emailToken);
        void Persist(TGUserEmailValidation _emailValidation);
        void SendUserValidationEmail(TGUser _tgUser, TGUserEmailValidation _tgUserEmailValidation);

        void LogWarning(Guid _userGuid, string _message);
        void LogException(Guid _userGuid, Exception _message);
        void LogInfo(Guid _userGuid, string _message);
        void LogVerbose(Guid _userGuid, string _message);
    }
}
