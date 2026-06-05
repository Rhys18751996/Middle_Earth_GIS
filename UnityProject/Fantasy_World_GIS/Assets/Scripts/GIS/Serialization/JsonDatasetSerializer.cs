using UnityEngine;

namespace Fantasy_World_GIS.GIS.Serialization
{
    public class JsonDatasetSerializer : IGisSerializer<SerializableDataset>
    {
        public string Serialize(SerializableDataset dataset)
        {
            return JsonUtility.ToJson(dataset, true);
        }

        public SerializableDataset Deserialize(string data)
        {
            return JsonUtility.FromJson<SerializableDataset>(data);
        }
    }
}