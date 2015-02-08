using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.DAOs
{
    public class TGUserDAO : AbstractMongoDAO<TGUser>
    {
        public TGUserDAO(MongoDatabase _mongoDB) : base(_mongoDB)
        {
            HasParent = false;
        }

        public override string TableName
        {
            get { return "TGUsers"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            BuildUniqueIndex("Username", "USERNAME");
            BuildUniqueSparceIndex("EmailAddress", "EMAIL");
        }

        public TGUser Get(string _username)
        {
            return GetOneItem<TGUser>("Username", _username);
        }

        public TGUser GetByEmail(string _emailAddress)
        {
            return GetOneItem<TGUser>("EmailAddress", _emailAddress);
        }
    }
}
