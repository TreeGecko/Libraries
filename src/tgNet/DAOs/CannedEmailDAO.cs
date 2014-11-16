using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.DAOs
{
    public class CannedEmailDAO : AbstractMongoDAO<CannedEmail>
    {
        public CannedEmailDAO(MongoDatabase _mongoDB) : base(_mongoDB)
        {
            HasParent = false;
        }

        public override string TableName
        {
            get { return "CannedEmail"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            BuildUniqueIndex("Name", "NAME");
        }

        public CannedEmail Get(string _name)
        {
            return GetOneItem<CannedEmail>("Name", _name);
        }
    }
}
