using Newtonsoft.Json;

namespace Capital.Placement.Api.Helper;

public static class Extension
{
    public static string ToJsonString(this object jsonObj)
    {
        return JsonConvert.SerializeObject(jsonObj);
    }
    
    public static TObject ToObject<TObject>(this string jsonString)
    {
        return JsonConvert.DeserializeObject<TObject>(jsonString);
    }
}