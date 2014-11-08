namespace TreeGecko.Library.Common.Objects
{
	public class NamedObject : AbstractTGObject
	{
		public string Name { get; set; }

		public string Identifier { get; set; }

		public int SequenceNumber { get; set; }
		
		public override void LoadFromTGSerializedObject (TGSerializedObject _tg)
		{
			base.LoadFromTGSerializedObject (_tg);
			
			Name = _tg.GetString("Name");
			Identifier = _tg.GetString("Identifier");
			SequenceNumber = _tg.GetInt32("SequenceNumber");
		}
		
		public override TGSerializedObject GetTGSerializedObject ()
		{
			TGSerializedObject tg = base.GetTGSerializedObject ();
			
			tg.Add("Name", Name);
			tg.Add("Identifier", Identifier);
			tg.Add("SequenceNumber", SequenceNumber);
			
			return tg;
		}
	}
}

