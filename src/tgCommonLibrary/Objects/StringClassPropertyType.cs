namespace TreeGecko.Library.Common.Objects
{
    public class StringClassPropertyType : ClassPropertyType
    {
        /// <summary>
        /// 
        /// </summary>
        public int MinimumLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MaximumLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StringClassPropertyType()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tg"></param>
        public override void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            base.LoadFromTGSerializedObject(_tg);

            MinimumLength = _tg.GetInt32("MinimumLength");
            MaximumLength = _tg.GetInt32("MaximumLength");
            DefaultValue = _tg.GetString("DefaultValue");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tg = base.GetTGSerializedObject();

            tg.Add("MinimumValue", MinimumLength);
            tg.Add("MaximumValue", MaximumLength);
            tg.Add("DefaultValue", DefaultValue);

            return tg;
        }
    }
}