using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.DAOs
{
    public class TGUserEmailValidationDAO: AbstractMongoDAO<TGUserEmailValidation>
    {
        public TGUserEmailValidationDAO(MongoDatabase _mongoDB) : base(_mongoDB)
        {
            //Parent is TGUser
            HasParent = true;
        }

        public override string TableName
        {
            get { return "TGUserEmailValidations"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            BuildUniqueIndex("ValidationText", "ValidationText");
        }

        public TGUserEmailValidation Get(string _validationText)
        {
            return GetOneItem<TGUserEmailValidation>("ValidationText", _validationText);
        }
    }
}
