using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Manages loading and unloading terrain chunks.
    /// Future streaming systems will use this class.
    /// </summary>
    public class TerrainChunkManager : MonoBehaviour
    {
        [SerializeField]
        private Material terrainMaterial;

        [SerializeField]
        private bool loadInitialChunksOnStart = true;

        [SerializeField]
        private Vector2Int initialChunk = new(1, 1);

        [SerializeField]
        private int initialLoadRadius = 1;

        private readonly Dictionary<Vector2Int, GameObject> loadedChunks = new();
        public int LoadedChunkCount => loadedChunks.Count;
        public ICollection<Vector2Int> LoadedChunks => loadedChunks.Keys;

        private void Start()
        {
            if (!loadInitialChunksOnStart)
            {
                return;
            }

            LoadChunkRadius(
                initialChunk.x,
                initialChunk.y,
                initialLoadRadius);
        }

        /// <summary>
        /// Loads a terrain chunk if it is not already loaded.
        /// </summary>
        public void LoadChunk(int chunkX, int chunkY)
        {
            Vector2Int chunkCoord = new Vector2Int(chunkX, chunkY);

            if (loadedChunks.ContainsKey(chunkCoord))
            {
                Debug.Log($"Chunk already loaded: {chunkCoord}");
                return;
            }

            string chunkPath = Application.dataPath + "/Data/Terrain/" + ChunkFileNaming.GetChunkFileName(chunkX,chunkY);

            if (!File.Exists(chunkPath))
            {
                Debug.Log($"Chunk file not found: {chunkPath}");
                return;
            }

            TerrainChunkData chunk = TerrainChunkLoader.Load(chunkPath);
            GameObject chunkObject = TerrainChunkRenderer.CreateChunk(chunk, terrainMaterial);
            chunkObject.transform.SetParent(transform, true);
            loadedChunks.Add(chunkCoord, chunkObject);

            Debug.Log($"Loaded chunk {chunk.EffectiveTileId}");
        }

        /// <summary>
        /// Loads all chunks within a radius.
        /// </summary>
        public void LoadChunkRadius(int centerX, int centerY, int radius)
        {
            for (int y = centerY-radius; y <= centerY+radius; y++)
            {
                for (int x = centerX-radius; x <= (centerX+radius); x++)
                {
                    LoadChunk(x, y);
                }
            }
        }

        /// <summary>
        /// Returns the set of chunk coordinates
        /// within a given radius.
        /// Used by future streaming logic.
        /// </summary>
        public HashSet<Vector2Int> GetChunkRadius(int centerChunkX, int centerChunkY, int radius)
        {
            HashSet<Vector2Int> chunks = new();

            for (int y = centerChunkY - radius; y <= centerChunkY + radius; y++)
            {
                for (int x = centerChunkX - radius; x <= centerChunkX + radius; x++)
                {
                    chunks.Add(new Vector2Int(x, y));
                }
            }

            return chunks;
        }

        /// <summary>
        /// Unloads a terrain chunk.
        /// </summary>
        public void UnloadChunk(int chunkX, int chunkY)
        {
            Vector2Int chunkCoord = new Vector2Int(chunkX, chunkY);

            if (!loadedChunks.TryGetValue(chunkCoord, out GameObject chunkObject))
            {
                return;
            }

            Destroy(chunkObject);
            loadedChunks.Remove(chunkCoord);

            Debug.Log($"Unloaded chunk {chunkCoord}");
        }

        /// <summary>
        /// Checks whether a chunk is currently loaded.
        /// </summary>
        public bool IsLoaded(int chunkX, int chunkY)
        {
            return loadedChunks.ContainsKey(new Vector2Int(chunkX, chunkY));
        }
    }
}