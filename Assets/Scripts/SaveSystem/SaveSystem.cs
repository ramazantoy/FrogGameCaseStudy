using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveSystem
    {
        private static Dictionary<string, string> SavableDataContainers = new();
        private static string Path => "FunradoCaseStudy";

        public static void DataSave(string data, string id)
        {
            SavableDataContainers[id] = data;
        }


        public static void SaveToFile()
        {
       var json = JsonConvert.SerializeObject(SavableDataContainers, Formatting.None);
       PlayerPrefs.SetString(Path, json);
        }

        public static void LoadFromFile()
        {
            var json = PlayerPrefs.GetString(Path, "{}");
          var save = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
          SavableDataContainers = save;
        }

        public static string Load(string id)
        {
            return SavableDataContainers.TryGetValue(id, value: out var load) ? load : null;
        }

    }


}