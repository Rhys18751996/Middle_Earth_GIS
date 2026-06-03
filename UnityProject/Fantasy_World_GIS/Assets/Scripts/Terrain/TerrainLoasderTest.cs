using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    public class TerrainLoaderTest : MonoBehaviour
    {
        private void Start()
        {
            string path =
                Application.dataPath +
                "/Data/Terrain/Chunk_000_000.json";

            TerrainChunkData chunk =
                TerrainChunkLoader.Load(path);

            Debug.Log(
                $"Loaded {chunk.ChunkId}");

            Debug.Log(
                $"Height Samples: {chunk.Heights.Length}");
        }
    }
}