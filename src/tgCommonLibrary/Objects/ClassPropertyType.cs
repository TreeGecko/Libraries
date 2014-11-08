namespace TreeGecko.Library.Common.Objects
{
    public class ClassPropertyType : AbstractTGObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string ShortQuestionText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LongQuestionText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tg"></param>
        public override void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            base.LoadFromTGSerializedObject(_tg);

            ShortQuestionText = _tg.GetString("ShortQuestionText");
            LongQuestionText = _tg.GetString("LongQuestionText");
            IsRequired = _tg.GetBoolean("IsRequired");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tg = base.GetTGSerializedObject();

            tg.Add("ShortQuestionText", ShortQuestionText);
            tg.Add("LongQuestionText", LongQuestionText);
            tg.Add("IsRequired", IsRequired);

            return tg;
        }
    }
}