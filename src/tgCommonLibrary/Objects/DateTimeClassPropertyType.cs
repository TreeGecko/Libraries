using System;

namespace TreeGecko.Library.Common.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class DateTimeClassPropertyType : ClassPropertyType
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime? MinimumDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? MaximumDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UseFixedDefault { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? FixedDefaultDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long RelativeDefaultDateTimeOffset { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tg"></param>
        public override void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            base.LoadFromTGSerializedObject(_tg);

            MinimumDateTime = _tg.GetNullableDateTime("MinimumDateTime");
            MaximumDateTime = _tg.GetNullableDateTime("MaximumDateTime");
            UseFixedDefault = _tg.GetBoolean("UseFixedDefault");
            FixedDefaultDateTime = _tg.GetNullableDateTime("FixedDefaultDateTime");
            RelativeDefaultDateTimeOffset = _tg.GetInt64("RelativeDefaultDateTimeOffset");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tg = base.GetTGSerializedObject();

            tg.Add("MinimumDateTime", MinimumDateTime);
            tg.Add("MaximumDateTime", MaximumDateTime);
            tg.Add("UseFixedDefault", UseFixedDefault);
            tg.Add("FixedDefaultDateTime", FixedDefaultDateTime);
            tg.Add("RelativeDefaultDateTimeOffset", RelativeDefaultDateTimeOffset);

            return tg;
        }
    }
}