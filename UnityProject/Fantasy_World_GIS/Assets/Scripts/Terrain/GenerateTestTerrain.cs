using System;
using System.IO;
using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    public class GenerateTestTerrain : MonoBehaviour
    {
        [SerializeField]
        private int width = 10;

        [SerializeField]
        private int height = 10;

        private void Start()
        {
            GenerateGrid(
                width,
                height);
        }

        public void GenerateGrid(
            int width,
            int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    GenerateChunk(
                        x,
                        y);
                }
            }

            Debug.Log(
                $"Generated {width * height} chunks");
        }

        public void GenerateChunk(
            int chunkX,
            int chunkY)
        {
            string path =
                Application.dataPath +
                "/Data/Terrain/" +
                ChunkFileNaming.GetHeightmapFileName(
                    chunkX,
                    chunkY);

            ushort[] heights =
                new ushort[257 * 257];

            for (int y = 0; y < 257; y++)
            {
                for (int x = 0; x < 257; x++)
                {
                    int index =
                        y * 257 + x;

                    float worldX =
                        x + (chunkX * 256);

                    float worldY =
                        y + (chunkY * 256);

                    float height =
                        Mathf.PerlinNoise(
                            worldX * 0.005f,
                            worldY * 0.005f) * 200f;

                    heights[index] =
                        (ushort)height;
                }
            }

            byte[] bytes =
                new byte[heights.Length * 2];

            Buffer.BlockCopy(
                heights,
                0,
                bytes,
                0,
                bytes.Length);

            File.WriteAllBytes(
                path,
                bytes);

            Debug.Log(
                $"Created {path}");

                // json files
                string jsonPath =
                    Application.dataPath +
                    "/Data/Terrain/" +
                    ChunkFileNaming.GetChunkFileName(
                        chunkX,
                        chunkY);

                TerrainChunkData chunkData =
                    new TerrainChunkData
                    {
                        ChunkId =
                            $"TERRAIN_{chunkX}_{chunkY}",

                        ChunkX =
                            chunkX,

                        ChunkY =
                            chunkY,

                        SampleCountX = 257,
                        SampleCountY = 257,

                        CellSize = 1.0f,

                        HeightMapFile =
                            ChunkFileNaming.GetHeightmapFileName(
                                chunkX,
                                chunkY)
                    };

                string json =
                    JsonUtility.ToJson(
                        chunkData,
                        true);

                File.WriteAllText(
                    jsonPath,
                    json);

                    Debug.Log($"Created {path}");
        }
    }
}