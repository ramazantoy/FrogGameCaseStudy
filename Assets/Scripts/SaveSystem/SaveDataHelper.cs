

using Newtonsoft.Json;

namespace SaveSystem
{
    public static class SaveDataHelper
    {
        public static void Save(this object data, string id)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
           SaveSystem.DataSave(json, id);
        }

        public static T Load<T>(string id, T defaultData) where T : class
        {
            var json = SaveSystem.Load(id);
            return json != null ? JsonConvert.DeserializeObject<T>(json) : defaultData;
        }

        public static void Save(this bool data, string id)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
           SaveSystem.DataSave(json, id);
        }
  
        public static bool Load(string id,bool defaultData)
        {
            var json = SaveSystem.Load(id);
            return json != null ? JsonConvert.DeserializeObject<bool>(json) : defaultData;
        }
    }
}