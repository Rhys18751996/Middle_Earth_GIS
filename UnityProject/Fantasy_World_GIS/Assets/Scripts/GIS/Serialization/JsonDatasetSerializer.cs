using UnityEngine;
using Fantasy_World_GIS.GIS.Core;

namespace Fantasy_World_GIS.GIS.Serialization
{
    public class JsonDatasetSerializer : IGisSerializer<GisDataset>
    {
        public string Serialize(GisDataset dataset)
        {
            return JsonUtility.ToJson(dataset, true);
        }

        public GisDataset Deserialize(string data)
        {
            return JsonUtility.FromJson<SerializableDataset>(data);
        }
    }
}