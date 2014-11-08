using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.DAOs
{
    public class WebLogEntryDAO: AbstractMongoDAO<CannedEmail>
    {
        public WebLogEntryDAO(MongoDatabase _mongoDB) : base(_mongoDB)
        {
            HasParent = false;
        }

        public override string TableName
        {
            get { return "WebLogEntries"; }
        }
    }
}
