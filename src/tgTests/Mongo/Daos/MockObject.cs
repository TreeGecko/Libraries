using System;
using TreeGecko.Library.Common.Interfaces;
using TreeGecko.Library.Common.Objects;

namespace tgTests.Mongo.Daos
{
    public class MockObject : AbstractTGObject, INamedObject
    {
        public Guid MockObjectGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            base.LoadFromTGSerializedObject(_tg);

            MockObjectGuid = _tg.GetGuid("MockObjectGuid");
            Name = _tg.GetString("Name");
            Description = _tg.GetString("Description");
        }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("MockObjectGuid", MockObjectGuid);
            tgs.Add("Name", Name);
            tgs.Add("Description", Description);

            return tgs;
        }
    }
}
