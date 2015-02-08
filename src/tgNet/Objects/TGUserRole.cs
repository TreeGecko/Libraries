using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Net.Objects
{
    public class TGUserRole : AbstractTGObject
    {
        public string Name { get; set; }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Name = _tgs.GetString("Name");
        }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();
            
            tgs.Add("Name", Name);

            return tgs;
        }
    }
}
