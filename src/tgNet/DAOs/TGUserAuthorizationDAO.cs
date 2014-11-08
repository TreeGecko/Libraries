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


    }
}
