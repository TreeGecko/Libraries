namespace TreeGecko.Library.Common.Objects
{
	public class AppSetting : NamedObject
	{
        public string Value { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tg = base.GetTGSerializedObject();
            tg.Add("Value", Value);
            return tg;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            base.LoadFromTGSerializedObject(_tg);
            Value = _tg.GetString("Value");
        }

	}
}

