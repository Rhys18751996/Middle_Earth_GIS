using UnityEngine;
using System;
using System.Collections.Generic;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Converts world positions into chunk coordinates.
    /// Future versions will manage terrain streaming.
    /// </summary>
    public class TerrainStreamingSystem : MonoBehaviour
    {
        private const int ChunkSize = 256;
        public Vector2Int CurrentChunk => currentChunk;

        [SerializeField]
        private TerrainChunkManager chunkManager;

        private Vector2Int currentChunk = new Vector2Int(int.MinValue,int.MinValue);

        public Vector2Int GetChunkCoordinate(Vector3 worldPosition)
        {
            int chunkX =
                Mathf.FloorToInt(
                    worldPosition.x /
                    ChunkSize);

            int chunkY =
                Mathf.FloorToInt(
                    worldPosition.z /
                    ChunkSize);

            return new Vector2Int(
                chunkX,
                chunkY);
        }

        public bool HasChunkChanged(Vector3 worldPosition)
        {
            Vector2Int newChunk =
                GetChunkCoordinate(
                    worldPosition);

            if (newChunk == currentChunk)
            {
                return false;
            }

            currentChunk = newChunk;

            return true;
        }

        [SerializeField]
        private int loadRadius = 1;


        public void LoadChunksAroundPosition(Vector3 worldPosition,int radius,TerrainChunkManager chunkManager)
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
                if (!HasChunkChanged(worldPosition))
                {
                    return;
                }

                Debug.Log(
                    $"Entered Chunk {currentChunk}");

                Debug.Log(
                    $"Entered Chunk {currentChunk} " +
                    $"Loaded Chunks: {chunkManager.LoadedChunkCount}");

                HashSet<Vector2Int> requiredChunks =
                    chunkManager.GetChunkRadius(
                        currentChunk.x,
                        currentChunk.y,
                        loadRadius);

                HashSet<Vector2Int> chunksToLoad =
                    new(requiredChunks);

                chunksToLoad.ExceptWith(
                    chunkManager.LoadedChunks);

                HashSet<Vector2Int> chunksToUnload =
                    new(chunkManager.LoadedChunks);

                chunksToUnload.ExceptWith(
                    requiredChunks);

                Debug.Log(
                    $"Chunks To Load: {chunksToLoad.Count}");

                Debug.Log(
                    $"Chunks To Unload: {chunksToUnload.Count}");

                foreach (Vector2Int chunk in chunksToLoad)
                {
                    chunkManager.LoadChunk(
                        chunk.x,
                        chunk.y);
                    Debug.Log($"LOAD {chunk}");
                }

                foreach (Vector2Int chunk in chunksToUnload)
                {
                    chunkManager.UnloadChunk(
                        chunk.x,
                        chunk.y);
                    Debug.Log($"UNLOAD {chunk}");
                }
            }
    }
}