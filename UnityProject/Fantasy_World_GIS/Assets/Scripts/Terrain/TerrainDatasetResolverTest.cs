using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    public class TerrainDatasetResolverTest : MonoBehaviour
    {
        private void Start()
        {
            TerrainDatasetRegistry registry =
                new TerrainDatasetRegistry();

            registry.LoadDatasets();

            TerrainDatasetResolver resolver =
                new TerrainDatasetResolver(
                    registry);

            TerrainDataset dataset =
                resolver.ResolveChunk(
                    1,
                    1);

            if (dataset == null)
            {
                Debug.Log(
                    "TerrainDatasetResolverTest: No dataset found.");

                return;
            }

            Debug.Log(
                $"TerrainDatasetResolverTest: Chunk (1,1) resolved to '{dataset.DatasetId}'");
        }
    }
}