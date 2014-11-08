namespace TreeGecko.Library.Common.Objects
{
    public class DoubleClassPropertyType : ClassPropertyType
    {
        public double? MinimumValue { get; set; }

        public double? MaximumValue { get; set; }

        public double? DefaultValue { get; set; }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            base.LoadFromTGSerializedObject(_tg);

            MinimumValue = _tg.GetNullableDouble("MinimumValue");
            MaximumValue = _tg.GetNullableDouble("MaximumValue");
            DefaultValue = _tg.GetNullableDouble("DefaultValue");
        }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tg = base.GetTGSerializedObject();

            tg.Add("MinimumValue", MinimumValue);
            tg.Add("MaximumValue", MaximumValue);
            tg.Add("DefaultValue", DefaultValue);

            return tg;
        }
    }
}