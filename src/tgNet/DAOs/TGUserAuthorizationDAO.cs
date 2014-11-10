using System;
using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.DAOs
{
    public class TGUserAuthorizationDAO: AbstractMongoDAO<TGUserAuthorization>
    {
        public TGUserAuthorizationDAO(MongoDatabase _mongoDB) : base(_mongoDB)
        {
            //Parent is TGUser
            HasParent = true;
        }

        public override string TableName
        {
            get { return "TGUserAuthorizations"; }
        }

        public TGUserAuthorization Get(Guid _userGuid, string _authToken)
        {
            TGUserAuthorization authorization = GetOneItem<TGUserAuthorization>("AuthorizationToken", _authToken);

            if (_userGuid.Equals(authorization.ParentGuid))
            {
                return authorization;
            }

            return null;
        }

        public override void BuildTable()
        {
            base.BuildTable();

            BuildUniqueIndex("AuthorizationToken", "TOKEN");
        }
    }
}
