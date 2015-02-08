using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TreeGecko.Library.Mongo.DAOs;
using TreeGecko.Library.Mongo.Helpers;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.DAOs
{
    public class TGEulaDAO : AbstractMongoDAO<TGEula>
    {
        public TGEulaDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            HasParent = false;
        }

        public override string TableName
        {
            get { return "TGEula"; }
        }

        public TGEula GetLatest()
        {
            IMongoQuery query = MongoQueryHelper.GetQuery();
            IMongoSortBy sortBy = new SortByBuilder().Descending("LastModifiedDateTime");
            MongoCursor cursor = MongoCollection.Find(query).SetSortOrder(sortBy);

            return GetOneItem<TGEula>(cursor);
        }
    } 
}
