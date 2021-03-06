﻿using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.DAOs
{
    public class SystemEmailDAO : AbstractMongoDAO<SystemEmail>
    {
        public SystemEmailDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            HasParent = false;
        }

        public override string TableName
        {
            get { return "SystemEmail"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            BuildNonuniqueIndex("SentDateTime", "SENT");
        }
    }
}
