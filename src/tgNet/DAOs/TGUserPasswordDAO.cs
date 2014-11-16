using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.DAOs
{
    public class TGUserPasswordDAO: AbstractMongoDAO<TGUserPassword>
    {
        public TGUserPasswordDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            //WHile parent could have been user typically parents have multiple children. 
            //Only one to one is desired at the moment
            HasParent = false;
        }

        public override string TableName
        {
            get { return "TGUserPassword"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            BuildUniqueIndex("UserGuid", "USERGUID");
        }
    }
}
