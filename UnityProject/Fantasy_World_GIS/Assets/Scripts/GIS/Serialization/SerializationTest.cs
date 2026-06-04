using UnityEngine;

namespace Fantasy_World_GIS.GIS.Serialization
{
    public class SerializationTest : MonoBehaviour
    {
        private void Start()
        {
            SerializableDataset dataset =
                new()
                {
                    DatasetId = "TEST_DATASET",
                    Name = "Test Dataset",
                    Description = "Serialization Test",
                    SchemaVersion = 1
                };

            JsonDatasetSerializer serializer = new();
            string json = serializer.Serialize(dataset);

            Debug.Log(json);
            SerializableDataset loaded = serializer.Deserialize(json);
            Debug.Log($"Loaded Dataset: " + $"{loaded.DatasetId}");
        }
    }
}