using System;
using TreeGecko.Library.Common.Attributes;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;

namespace TreeGecko.Library.Common.Objects
{
    public abstract class AbstractTGObject : ITGSerializable, IActive
    {
        protected AbstractTGObject()
        {
            Guid = Guid.NewGuid();
            Active = true;
            LastModifiedDateTime = DateTime.UtcNow;
        }

        /// <summary>
        /// 
        /// </summary>
        [InternalTGObjectGuid]
        public Guid Guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid VersionGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VersionTimeStamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the last modified date time.
        /// </summary>
        /// <value>
        /// The last modified date time.
        /// </value>
        public DateTime LastModifiedDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the parent GUID.
        /// </summary>
        /// <value>
        /// The parent GUID.
        /// </value>
        public Guid? ParentGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PersistedDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject obj = new TGSerializedObject
            {
                TGObjectType = TGObjectType
            };

            obj.Add("Guid", Guid);
            obj.Add("Active", Active);
            obj.Add("LastModifiedBy", LastModifiedBy);
            obj.Add("LastModifiedDateTime", LastModifiedDateTime);
            obj.Add("VersionTimeStamp", VersionTimeStamp);
            obj.Add("ParentGuid", ParentGuid);
            obj.Add("VersionGuid", VersionGuid);
            obj.Add("PersistedDateTime", PersistedDateTime);

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tg"></param>
        public virtual void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            Guid = _tg.GetGuid("Guid");
            if (Guid == Guid.Empty)
            {
                Guid = Guid.NewGuid();
            }

            Active = _tg.GetBoolean("Active");
            LastModifiedDateTime = _tg.GetDateTime("LastModifiedDateTime");
            LastModifiedBy = _tg.GetNullableGuid("LastModifiedBy");

            string versionTimeStamp = _tg.GetString("VersionTimeStamp");

            if (VersionTimeStamp == null)
            {
                VersionTimeStamp = DateHelper.GetCurrentTimeStamp();
            }
            else
            {
                VersionTimeStamp = versionTimeStamp;
            }

            ParentGuid = _tg.GetNullableGuid("ParentGuid");
            VersionGuid = _tg.GetGuid("VersionGuid");
            PersistedDateTime = _tg.GetDateTime("PersistedDateTime");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_bcnSerializedString"></param>
        public void LoadFromTGSerializedString(string _bcnSerializedString)
        {
            TGSerializedObject tgSerializedObject = new TGSerializedObject(_bcnSerializedString);
            LoadFromTGSerializedObject(tgSerializedObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            TGSerializedObject obj = GetTGSerializedObject();

            return obj.ToString();
        }

        public virtual string TGObjectType
        {
            get { return ReflectionHelper.GetTypeName(GetType()); }
        }
    }
}