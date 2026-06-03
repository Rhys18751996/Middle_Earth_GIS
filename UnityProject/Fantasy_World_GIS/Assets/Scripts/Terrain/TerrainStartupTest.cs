using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    public class TerrainStartupTest : MonoBehaviour
    {
        [SerializeField]
        private Material terrainMaterial;

        private void Start()
        {
            string chunkPath =
                Application.dataPath +
                "/Data/Terrain/Chunk_000_000.json";

            TerrainChunkData chunk =
                TerrainChunkLoader.Load(chunkPath);

            TerrainChunkRenderer.CreateChunk(
                chunk,
                terrainMaterial);

            Debug.Log($"Loaded {chunk.ChunkId}");
        }
    }
}