using System;
using System.Collections.Specialized;
using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.DAOs
{
    public class TGUserRoleDAO : AbstractMongoDAO<TGUserRole>
    {
        public TGUserRoleDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            //TGUser (or other user as required).
            HasParent = true;
        }

        public override string TableName
        {
            get { return "TGUserRole"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();


            BuildUniqueIndex(new []{"ParentGuid", "Name"}, "USER_ROLE");
        }

        public TGUserRole Get(Guid _userGuid, string _roleName)
        {
            NameValueCollection nvc = new NameValueCollection
            {
                {"ParentGuid", _userGuid.ToString()}, 
                {"Name", _roleName}
            };

            return GetOneItem<TGUserRole>(nvc);
        }

        public bool HasRole(Guid _userGuid, string _roleName)
        {
            TGUserRole role = Get(_userGuid, _roleName);

            if (role != null)
            {
                return true;
            }

            return false;
        }
    }
}
