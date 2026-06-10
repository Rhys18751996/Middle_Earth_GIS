using UnityEngine;
using System.Collections.Generic;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Controls terrain streaming around a moving position.
    ///
    /// Converts world positions into streaming-grid coordinates
    /// and determines which chunks should be loaded or unloaded
    /// as the observer moves through the world.
    ///
    /// The streaming system is independent of dataset resolution;
    /// it operates purely on chunk coordinates.
    /// </summary>
    public class TerrainStreamingSystem : MonoBehaviour
    {
        [SerializeField]
        private TerrainChunkManager chunkManager;

        [SerializeField]
        private int loadRadius = 10;

        [SerializeField]
        private bool loadAroundTransformOnStart = true;

        /// <summary>
        /// Temporary streaming grid size.
        /// This is NOT dataset resolution.
        /// This is NOT tile width.
        /// It simply defines the world-space streaming grid.
        /// </summary>
        private const float StreamingGridSize = 256f;

        public Vector2Int CurrentChunk => currentChunk;

        private Vector2Int currentChunk = new(int.MinValue, int.MinValue);

        private void Awake()
        {
            if (chunkManager == null)
            {
                chunkManager = FindFirstObjectByType<TerrainChunkManager>();
            }
        }

        private void Start()
        {
            if (loadAroundTransformOnStart)
            {
                UpdateStreaming(transform.position);
            }
        }

        public Vector2Int GetChunkCoordinate(Vector3 worldPosition)
        {
            int chunkX = Mathf.FloorToInt(worldPosition.x / StreamingGridSize);
            int chunkY = Mathf.FloorToInt(worldPosition.z / StreamingGridSize);

            return new Vector2Int(chunkX, chunkY);
        }

        public bool HasChunkChanged(Vector3 worldPosition)
        {
            Vector2Int newChunk = GetChunkCoordinate(worldPosition);

            if (newChunk == currentChunk)
            {
                return false;
            }

            currentChunk = newChunk;

            return true;
        }

        public void LoadChunksAroundPosition(
            Vector3 worldPosition,
            int radius,
            TerrainChunkManager chunkManager)
        {
            Vector2Int chunk =
                GetChunkCoordinate(
                    worldPosition);

            chunkManager.LoadChunkRadius(
                chunk.x,
                chunk.y,
                radius);
        }

        public void UpdateStreaming(
            Vector3 worldPosition)
        {
            if (chunkManager == null)
            {
                Debug.LogWarning(
                    "Terrain streaming cannot load chunks without a TerrainChunkManager.");

                return;
            }

            if (!HasChunkChanged(
                    worldPosition))
            {
                return;
            }

            HashSet<Vector2Int> requiredChunks =
                chunkManager.GetChunkRadius(
                    currentChunk.x,
                    currentChunk.y,
                    loadRadius);

            HashSet<Vector2Int> chunksToLoad = new(requiredChunks);

            chunksToLoad.ExceptWith(chunkManager.LoadedChunks);

            HashSet<Vector2Int> chunksToUnload = new(chunkManager.LoadedChunks);

            chunksToUnload.ExceptWith(requiredChunks);

            foreach (Vector2Int chunk in chunksToLoad)
            {
                chunkManager.LoadChunk(
                    chunk.x,
                    chunk.y);
            }

            foreach (Vector2Int chunk in chunksToUnload)
            {
                chunkManager.UnloadChunk(
                    chunk.x,
                    chunk.y);
            }
        }
    }
}