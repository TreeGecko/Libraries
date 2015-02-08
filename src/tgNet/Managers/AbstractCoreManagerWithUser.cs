using System;
using System.Collections.Generic;
using MongoDB.Driver;
using TreeGecko.Library.Net.DAOs;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.Managers
{
    public class AbstractCoreManagerWithUser : AbstractCoreManager
    {
        public AbstractCoreManagerWithUser(MongoDatabase _mongoDB) 
            : base(_mongoDB)
        {
        }

        public AbstractCoreManagerWithUser(string _name) 
            : base(_name)
        {
        }

        #region Users
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_user"></param>
        public void Persist(TGUser _user)
        {
            TGUserDAO dao = new TGUserDAO(MongoDB);
            dao.Persist(_user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userGuid"></param>
        /// <returns></returns>
        public TGUser GetUser(Guid _userGuid)
        {
            TGUserDAO dao = new TGUserDAO(MongoDB);
            return dao.Get(_userGuid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_username"></param>
        /// <returns></returns>
        public TGUser GetUser(string _username)
        {
            TGUserDAO dao = new TGUserDAO(MongoDB);
            return dao.Get(_username);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TGUser> GetUsers()
        {
            TGUserDAO dao = new TGUserDAO(MongoDB);
            return dao.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_emailAddress"></param>
        /// <returns></returns>
        public TGUser GetUserByEmail(string _emailAddress)
        {
            TGUserDAO dao = new TGUserDAO(MongoDB);
            return dao.GetByEmail(_emailAddress);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public long GetUserCount()
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_username"></param>
        /// <param name="_authorizationToken"></param>
        /// <param name="_user"></param>
        /// <returns></returns>
        public bool ValidateUser(string _username, string _authorizationToken, out TGUser _user)
        {
            _user = GetUser(_username);

            if (_user != null)
            {
                TGUserAuthorization userAuthorization = GetUserAuthorization(_user.Guid, _authorizationToken);

                if (userAuthorization != null)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion


    }
}
