using System;
using System.Collections.Generic;
using MongoDB.Driver;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Mongo.Managers;
using TreeGecko.Library.Net.DAOs;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.Managers
{
    public abstract class AbstractCoreManager : AbstractMongoManager
    {
        protected AbstractCoreManager(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
        }

        protected AbstractCoreManager(string _name)
            : base(_name)
        {
        }

        #region UserPassword
        public bool ValidateUser(TGUser _user, string _testPassword)
        {
            TGUserPassword userPassword = GetTGUserPassword(_user.Guid);

            if (userPassword != null)
            {
                string testHash = TGUserPassword.HashPassword(userPassword.Salt, _testPassword);

                if (testHash.Equals(userPassword.HashedPassword))
                {
                    return true;
                }
            }

            return false;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="_guid"></param>
        /// <returns></returns>
        public TGUserPassword GetTGUserPassword(Guid _guid)
        {
            TGUserPasswordDAO dao = new TGUserPasswordDAO(MongoDB);
            return dao.Get(_guid);
        }

        public TGUserPassword GetUserPasswordByUser(Guid _userGuid)
        {
            TGUserPasswordDAO dao = new TGUserPasswordDAO(MongoDB);
            return dao.GetOneItem<TGUserPassword>("UserGuid", _userGuid.ToString());
        }

        public void Persist(TGUserPassword _userPassword)
        {
            TGUserPasswordDAO dao = new TGUserPasswordDAO(MongoDB);
            dao.Persist(_userPassword);
        }
        #endregion

        #region UserEmailValidation

        public void Delete(TGUserEmailValidation _userEmailValidation)
        {
            TGUserEmailValidationDAO dao = new TGUserEmailValidationDAO(MongoDB);
            dao.Delete(_userEmailValidation);
        }

        public TGUserEmailValidation GetTGUserEmailValidation(string _emailToken)
        {
            TGUserEmailValidationDAO dao = new TGUserEmailValidationDAO(MongoDB);
            return dao.Get(_emailToken);
        }

        public void Persist(TGUserEmailValidation _emailValidation)
        {
            TGUserEmailValidationDAO dao = new TGUserEmailValidationDAO(MongoDB);
            dao.Persist(_emailValidation);
        }
        #endregion

        #region UserAuthorizations

        public TGUserAuthorization GetUserAuthorization(Guid _userGuid, string _authorizationToken)
        {
            TGUserAuthorizationDAO dao = new TGUserAuthorizationDAO(MongoDB);
            return dao.Get(_userGuid, _authorizationToken);
        }

        public void Persist(TGUserAuthorization _tgUserAuthorization)
        {
            TGUserAuthorizationDAO dao = new TGUserAuthorizationDAO(MongoDB);
            dao.Persist(_tgUserAuthorization);
        }

        public string GetAuthorizationToken(Guid _userGuid, string _deviceType)
        {
            TGUserAuthorization userAuthorization = TGUserAuthorization.GetNew(_userGuid, _deviceType);
            Persist(userAuthorization);

            return userAuthorization.AuthorizationToken;
        }

        #endregion

        #region Logging
        public void LogWarning(Guid _userGuid, string _message)
        {
            TraceFileHelper.Warning(_message + "|UserGuid - {0}", _userGuid);
        }

        public void LogException(Guid _userGuid, Exception _message)
        {
            TraceFileHelper.Exception(_message + "|UserGuid - {0}", _userGuid);
        }

        public void LogInfo(Guid _userGuid, string _message)
        {
            TraceFileHelper.Info(_message + "|UserGuid - {0}", _userGuid);
        }

        public void LogVerbose(Guid _userGuid, string _message)
        {
            TraceFileHelper.Verbose(_message + "|UserGuid - {0}", _userGuid);
        }

        #endregion

        #region Email
        public void Persist(CannedEmail _cannedEmail)
        {
            CannedEmailDAO dao = new CannedEmailDAO(MongoDB);
            dao.Persist(_cannedEmail);
        }

        public CannedEmail GetCannedEmail(Guid _cannedEmailGuid)
        {
            CannedEmailDAO dao = new CannedEmailDAO(MongoDB);
            return dao.Get(_cannedEmailGuid);
        }

        public List<CannedEmail> GetCannedEmails()
        {
            CannedEmailDAO dao = new CannedEmailDAO(MongoDB);
            return dao.GetAll();
        }

        public List<CannedEmail> GetActiveCannedEmails()
        {
            CannedEmailDAO dao = new CannedEmailDAO(MongoDB);
            return dao.GetActive();
        }

        public CannedEmail GetCannedEmail(string _cannedEmailName)
        {
            CannedEmailDAO dao = new CannedEmailDAO(MongoDB);
            return dao.Get(_cannedEmailName);
        }

        public void Persist(SystemEmail _systemEmail)
        {
            SystemEmailDAO dao = new SystemEmailDAO(MongoDB);
            dao.Persist(_systemEmail);
        }

        public SystemEmail GetSystemEmail(Guid _guid)
        {
            SystemEmailDAO dao = new SystemEmailDAO(MongoDB);
            return dao.Get(_guid);
        }

        #endregion

        #region Eula

        public TGEula GetEula(Guid _eulaGuid)
        {
            TGEulaDAO dao = new TGEulaDAO(MongoDB);
            return dao.Get(_eulaGuid);
        }

        public void Persist(TGEula _eula)
        {
            TGEulaDAO dao = new TGEulaDAO(MongoDB);
            dao.Persist(_eula);
        }

        public TGEula GetLatestEula()
        {
            TGEulaDAO dao = new TGEulaDAO(MongoDB);
            return dao.GetLatest();
        }

        public TGEulaAgreement GetEulaAgreement(Guid _userGuid, Guid _eulaGuid)
        {
            TGEulaAgreementDAO dao = new TGEulaAgreementDAO(MongoDB);
            return dao.Get(_userGuid, _eulaGuid);
        }

        public void Persist(TGEulaAgreement _eulaAgreement)
        {
            TGEulaAgreementDAO dao = new TGEulaAgreementDAO(MongoDB);
            dao.Persist(_eulaAgreement);
        }

        #endregion

        #region UserRole

        public void Persist(TGUserRole _userRole)
        {
            TGUserRoleDAO dao = new TGUserRoleDAO(MongoDB);
            dao.Persist(_userRole);
        }

        public TGUserRole GetUserRole(Guid _userRoleGuid)
        {
            TGUserRoleDAO dao = new TGUserRoleDAO(MongoDB);
            return dao.Get(_userRoleGuid);
        }

        public List<TGUserRole> GetUserRoles(Guid _userGuid)
        {
            TGUserRoleDAO dao = new TGUserRoleDAO(MongoDB);
            return dao.GetChildrenOf(_userGuid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userGuid"></param>
        /// <param name="_roleName">Case Sensitive</param>
        /// <returns></returns>
        public TGUserRole GetUserRole(Guid _userGuid, string _roleName)
        {
            TGUserRoleDAO dao = new TGUserRoleDAO(MongoDB);
            return dao.Get(_userGuid, _roleName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userGuid"></param>
        /// <param name="_roleName">Case Sensitive</param>
        /// <returns></returns>
        public bool HasUserRole(Guid _userGuid, string _roleName)
        {
            TGUserRoleDAO dao = new TGUserRoleDAO(MongoDB);
            return dao.HasRole(_userGuid, _roleName);
        }

        #endregion
    }
}
