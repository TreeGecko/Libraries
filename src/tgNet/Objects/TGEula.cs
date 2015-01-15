using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Net.Objects
{
    public class TGEula : AbstractTGObject
    {
        public string Text { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("Text", Text);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Text = _tgs.GetString("Text");
        }
    }
}
