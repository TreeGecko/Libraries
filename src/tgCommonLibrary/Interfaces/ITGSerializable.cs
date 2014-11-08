using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Common.Interfaces
{
    public interface ITGSerializable
    {
        TGSerializedObject GetTGSerializedObject();
        void LoadFromTGSerializedObject(TGSerializedObject _tg);
        string TGObjectType { get; }
    }
}
