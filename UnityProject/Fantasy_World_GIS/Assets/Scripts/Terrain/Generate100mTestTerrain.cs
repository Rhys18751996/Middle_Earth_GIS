using System;
using System.IO;
using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    public class Generate100mTestTerrain : MonoBehaviour
    {
        private const string DatasetId =
            "MiddleEarth_100m_Test";

        private const float CellSize = 1f;//100f;

        private const int Priority =
            1;

        [Header("Coverage")]

        [SerializeField]
        private int width = 7;

        [SerializeField]
        private int height = 7;

        [Header("Terrain")]

        [SerializeField]
        private float minHeight = 0f;

        [SerializeField]
        private float maxHeight = 1200f;

        [SerializeField]
        private float PerlinNoise01 = 200f;
        [SerializeField]
        private float PerlinNoise04 = 80f;
        [SerializeField]
        private float PerlinNoise02 = 20f;

        private void Start()
        {
            GenerateDataset();
        }

        private void GenerateDataset()
        {
            string datasetFolder =
                Path.Combine(
                    Application.dataPath,
                    "Data",
                    "Terrain",
                    DatasetId);

            Directory.CreateDirectory(
                datasetFolder);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    GenerateChunk(
                        datasetFolder,
                        x,
                        y);
                }
            }

            WriteManifest(
                datasetFolder);

            Debug.Log(
                $"Generated dataset '{DatasetId}'");
        }

        private void GenerateChunk(
            string datasetFolder,
            int chunkX,
            int chunkY)
        {
            string heightMapPath =
                Path.Combine(
                    datasetFolder,
                    ChunkFileNaming.GetHeightmapFileName(
                        chunkX,
                        chunkY));

            ushort[] heights =
                new ushort[
                    TerrainConstants.DefaultTileSampleCount *
                    TerrainConstants.DefaultTileSampleCount];

            for (int y = 0; y < TerrainConstants.DefaultTileSampleCount; y++)
            {
                for (int x = 0; x < TerrainConstants.DefaultTileSampleCount; x++)
                {
                    int index =
                        y * TerrainConstants.DefaultTileSampleCount + x;

                    float worldX =
                        x +
                        (chunkX * TerrainConstants.ChunkSizeMeters);

                    float worldY =
                        y +
                        (chunkY * TerrainConstants.ChunkSizeMeters);

                    float heightValue =
                        GenerateHeight(
                            worldX,
                            worldY);

                    heights[index] =
                        (ushort)Mathf.Clamp(
                            heightValue,
                            minHeight,
                            maxHeight);
                }
            }

            byte[] bytes =
                new byte[
                    heights.Length *
                    sizeof(ushort)];

            Buffer.BlockCopy(
                heights,
                0,
                bytes,
                0,
                bytes.Length);

            File.WriteAllBytes(
                heightMapPath,
                bytes);

            string jsonPath =
                Path.Combine(
                    datasetFolder,
                    ChunkFileNaming.GetChunkFileName(
                        chunkX,
                        chunkY));

            TerrainChunkData chunkData =
                CreateTerrainChunkData(
                    chunkX,
                    chunkY);

            File.WriteAllText(
                jsonPath,
                JsonUtility.ToJson(
                    chunkData,
                    true));
        }

        private TerrainChunkData CreateTerrainChunkData(
            int chunkX,
            int chunkY)
        {
            string formattedX =
                ChunkFileNaming.FormatCoordinatePublic(
                    chunkX);

            string formattedY =
                ChunkFileNaming.FormatCoordinatePublic(
                    chunkY);

            double minX =
                chunkX *
                TerrainConstants.ChunkSizeMeters;

            double minY =
                chunkY *
                TerrainConstants.ChunkSizeMeters;

            double maxX =
                minX +
                TerrainConstants.ChunkSizeMeters;

            double maxY =
                minY +
                TerrainConstants.ChunkSizeMeters;

            return new TerrainChunkData
            {
                DatasetId = DatasetId,
                DatasetType = TerrainConstants.TerrainDatasetType,

                TileId =
                    $"Tile_{formattedX}_{formattedY}",

                TileX = chunkX,
                TileY = chunkY,

                ChunkId =
                    $"Tile_{formattedX}_{formattedY}",

                ChunkX = chunkX,
                ChunkY = chunkY,

                SampleCountX =
                    TerrainConstants.DefaultTileSampleCount,

                SampleCountY =
                    TerrainConstants.DefaultTileSampleCount,

                CellSize = CellSize,

                Bounds = new TerrainBounds(
                    minX,
                    minY,
                    maxX,
                    maxY),

                HeightFormat =
                    TerrainConstants.UInt16HeightFormat,

                MinElevation =
                    minHeight,

                MaxElevation =
                    maxHeight,

                Version = 1,

                HeightMapFile =
                    ChunkFileNaming.GetHeightmapFileName(
                        chunkX,
                        chunkY),

                Attributes =
                    new TerrainAttributes
                    {
                        Source =
                            "Procedural 100m test terrain",

                        HistoricalPeriod =
                            "Synthetic Test Data",

                        Author =
                            "Middle_Earth_GIS",

                        Notes =
                            "Generated 100m test terrain dataset."
                    }
            };
        }

        private float GenerateHeight(
            float worldX,
            float worldY)
        {
            float height = 0f;

            height +=
                Mathf.PerlinNoise(
                    worldX * 0.001f,
                    worldY * 0.001f) * PerlinNoise01;

            height +=
                Mathf.PerlinNoise(
                    worldX * 0.004f,
                    worldY * 0.004f) * PerlinNoise04;

            height +=
                Mathf.PerlinNoise(
                    worldX * 0.02f,
                    worldY * 0.02f) * PerlinNoise02;

            return height;
        }

        private void WriteManifest(
            string datasetFolder)
        {
            string manifestPath =
                Path.Combine(
                    datasetFolder,
                    "manifest.json");

            TerrainDatasetManifest manifest =
                new TerrainDatasetManifest
                {
                    DatasetId =
                        DatasetId,

                    DatasetType =
                        TerrainConstants.TerrainDatasetType,

                    CellSize =
                        CellSize,

                    Priority =
                        Priority,

                    Version = 1,

                    CoverageBounds =
                        new TerrainBounds(
                            0,
                            0,
                            width * TerrainConstants.ChunkSizeMeters,
                            height * TerrainConstants.ChunkSizeMeters),

                    TileSize =
                        TerrainConstants.DefaultTileSampleCount,

                    HeightFormat =
                        TerrainConstants.UInt16HeightFormat,

                    MinElevation =
                        minHeight,

                    MaxElevation =
                        maxHeight,

                    Attributes =
                        new TerrainAttributes
                        {
                            Source =
                                "Procedural 100m test terrain",

                            HistoricalPeriod =
                                "Synthetic Test Data",

                            Author =
                                "Middle_Earth_GIS",

                            Notes =
                                "Generated 100m test terrain dataset."
                        }
                };

            File.WriteAllText(
                manifestPath,
                JsonUtility.ToJson(
                    manifest,
                    true));
        }
    }
}