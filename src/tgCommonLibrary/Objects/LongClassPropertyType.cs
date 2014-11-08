using System;

namespace TreeGecko.Library.Common.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class LongClassPropertyType : ClassPropertyType
    {
        /// <summary>
        /// 
        /// </summary>
        public Int64? MinimumValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int64? MaximumValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int64? DefaultValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tg"></param>
        public override void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            base.LoadFromTGSerializedObject(_tg);

            MinimumValue = _tg.GetNullableInt64("MinimumValue");
            MaximumValue = _tg.GetNullableInt64("MaximumValue");
            DefaultValue = _tg.GetNullableInt64("DefaultValue");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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