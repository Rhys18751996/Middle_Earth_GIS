using UnityEngine;
using Fantasy_World_GIS.GIS.Core;

namespace Fantasy_World_GIS.GIS.Registries
{
    public class RegistryTest : MonoBehaviour
    {
        private void Start()
        {
            DatasetRegistry registry = new();

            RiverDataset dataset =
                new()
                {
                    DatasetId = "RIVERS_TEST",
                    Name = "Test Rivers"
                };

            registry.Register(dataset);

            bool found = registry.TryGetDataset("RIVERS_TEST", out GisDataset result);

            Debug.Log($"Found Dataset: {found}");
        }
    }
}