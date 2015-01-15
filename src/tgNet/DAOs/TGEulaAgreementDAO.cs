using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.DAOs
{
    public class TGEulaAgreementDAO : AbstractMongoDAO<TGEulaAgreement>
    {
        public TGEulaAgreementDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            HasParent = false;
        }

        public override string TableName
        {
            get { return "TGEulaAgreement"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            List<string> columns = new List<string>() { "UserGuid", "EulaGuid" };
            BuildUniqueIndex(columns, "USER_EULA");
        }

        public TGEulaAgreement Get(Guid _userGuid, Guid _eulaGuid)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("UserGuid", _userGuid.ToString());
            nvc.Add("EulaGuid", _eulaGuid.ToString());

            return GetOneItem<TGEulaAgreement>(nvc);
        }
    } 
}
