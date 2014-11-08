using Newtonsoft.Json;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Common.Helpers
{
    
    public static class JSONHelper
    {
        public static string GetJson(object _obj)
        {
            return JsonConvert.SerializeObject(_obj);
        }

        public static T GetObject<T>(string _json) where T : AbstractTGObject
        {
            return JsonConvert.DeserializeObject<T>(_json);
        }

    }
}