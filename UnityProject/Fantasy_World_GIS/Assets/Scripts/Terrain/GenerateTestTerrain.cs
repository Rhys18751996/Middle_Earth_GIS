using System;
using System.IO;
using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    public class GenerateTestTerrain : MonoBehaviour
    {
        private const string TestDatasetId = "MiddleEarth_1m_Test";

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

            WriteManifest(width, height);

            Debug.Log(
                $"Generated {width * height} terrain tiles");
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
                new ushort[TerrainConstants.DefaultTileSampleCount * TerrainConstants.DefaultTileSampleCount];

            for (int y = 0; y < TerrainConstants.DefaultTileSampleCount; y++)
            {
                for (int x = 0; x < TerrainConstants.DefaultTileSampleCount; x++)
                {
                    int index =
                        y * TerrainConstants.DefaultTileSampleCount + x;

                    float worldX =
                        x + (chunkX * TerrainConstants.ChunkSizeMeters);

                    float worldY =
                        y + (chunkY * TerrainConstants.ChunkSizeMeters);

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

            string jsonPath =
                Application.dataPath +
                "/Data/Terrain/" +
                ChunkFileNaming.GetChunkFileName(
                    chunkX,
                    chunkY);

            TerrainChunkData chunkData =
                CreateTerrainTileData(chunkX, chunkY);

            string json =
                JsonUtility.ToJson(
                    chunkData,
                    true);

            File.WriteAllText(
                jsonPath,
                json);

            Debug.Log($"Created {jsonPath}");
        }

        private static TerrainChunkData CreateTerrainTileData(int tileX, int tileY)
        {
            string formattedX = ChunkFileNaming.FormatCoordinatePublic(tileX);
            string formattedY = ChunkFileNaming.FormatCoordinatePublic(tileY);

            double minX = tileX * TerrainConstants.ChunkSizeMeters;
            double minY = tileY * TerrainConstants.ChunkSizeMeters;
            double maxX = minX + TerrainConstants.ChunkSizeMeters;
            double maxY = minY + TerrainConstants.ChunkSizeMeters;

            TerrainChunkData chunkData =
                new TerrainChunkData
                {
                    DatasetId = TestDatasetId,
                    DatasetType = TerrainConstants.TerrainDatasetType,
                    TileId = $"Tile_{formattedX}_{formattedY}",
                    TileX = tileX,
                    TileY = tileY,
                    ChunkId = $"Tile_{formattedX}_{formattedY}",
                    ChunkX = tileX,
                    ChunkY = tileY,
                    SampleCountX = TerrainConstants.DefaultTileSampleCount,
                    SampleCountY = TerrainConstants.DefaultTileSampleCount,
                    CellSize = 1.0f,
                    Bounds = new TerrainBounds(minX, minY, maxX, maxY),
                    HeightFormat = TerrainConstants.UInt16HeightFormat,
                    MinElevation = 0f,
                    MaxElevation = 200f,
                    Version = 1,
                    HeightMapFile = ChunkFileNaming.GetHeightmapFileName(tileX, tileY),
                    Attributes = new TerrainAttributes
                    {
                        Source = "Procedural Perlin test terrain",
                        HistoricalPeriod = "Synthetic Test Data",
                        Author = "Middle_Earth_GIS",
                        Notes = "Representative Phase 2 sample terrain tile."
                    }
                };

            return chunkData;
        }

        private static void WriteManifest(int width, int height)
        {
            string manifestPath =
                Application.dataPath +
                "/Data/Terrain/" +
                TestDatasetId +
                ".manifest.json";

            TerrainDatasetManifest manifest =
                new TerrainDatasetManifest
                {
                    DatasetId = TestDatasetId,
                    DatasetType = TerrainConstants.TerrainDatasetType,
                    CellSize = 1.0f,
                    Priority = 1,
                    Version = 1,
                    CoverageBounds = new TerrainBounds(
                        0,
                        0,
                        width * TerrainConstants.ChunkSizeMeters,
                        height * TerrainConstants.ChunkSizeMeters),
                    TileSize = TerrainConstants.DefaultTileSampleCount,
                    HeightFormat = TerrainConstants.UInt16HeightFormat,
                    MinElevation = 0f,
                    MaxElevation = 200f,
                    Attributes = new TerrainAttributes
                    {
                        Source = "Procedural Perlin test terrain",
                        HistoricalPeriod = "Synthetic Test Data",
                        Author = "Middle_Earth_GIS",
                        Notes = "Generated test dataset manifest for Phase 2 terrain representation."
                    }
                };

            File.WriteAllText(
                manifestPath,
                JsonUtility.ToJson(manifest, true));
        }
    }
}
