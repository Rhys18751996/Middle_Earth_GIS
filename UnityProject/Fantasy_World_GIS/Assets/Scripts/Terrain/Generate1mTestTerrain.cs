using System;
using System.IO;

using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    public class Generate1mTestTerrain : MonoBehaviour
    {
        private const string DatasetId = "MiddleEarth_1m_Test";

        private const float ResolutionMeters = 1f;

        private const int Priority =
            100;

        [Header("Coverage")]
        [SerializeField]
        private int startTileX = 3;

        [SerializeField]
        private int startTileY = 3;

        [SerializeField]
        private int width = 3;

        [SerializeField]
        private int height = 3;

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

        private float TileWidthMeters => TerrainConstants.ChunkSizeMeters;

        private int SampleCount =>
            Mathf.RoundToInt(
                TerrainConstants.ChunkSizeMeters /
                ResolutionMeters) + 1;

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

            Directory.CreateDirectory(datasetFolder);

            for (int y = startTileY; y < startTileY + height; y++)
            {
                for (int x = startTileX; x < startTileX + width; x++)
                {
                    GenerateChunk(datasetFolder, x, y);
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
                    SampleCount *
                    SampleCount];

            for (int y = 0; y < SampleCount; y++)
            {
                for (int x = 0; x < SampleCount; x++)
                {
                    int index =
                        y * SampleCount + x;

                    float worldX =
                        (x * ResolutionMeters) +
                        (chunkX * TileWidthMeters);

                    float worldY =
                        (y * ResolutionMeters) +
                        (chunkY * TileWidthMeters);

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
                chunkX * TileWidthMeters;

            double minY =
                chunkY * TileWidthMeters;

            double maxX =
                minX + TileWidthMeters;

            double maxY =
                minY + TileWidthMeters;

            return new TerrainChunkData
            {
                DatasetId = DatasetId,

                DatasetType =
                    TerrainConstants.TerrainDatasetType,

                TileId =
                    $"Tile_{formattedX}_{formattedY}",

                TileX = chunkX,
                TileY = chunkY,

                ChunkId =
                    $"Tile_{formattedX}_{formattedY}",

                ChunkX = chunkX,
                ChunkY = chunkY,

                SampleCountX = SampleCount,
                SampleCountY = SampleCount,

                ResolutionMeters =
                    ResolutionMeters,

                Bounds =
                    new TerrainBounds(
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
                            "Procedural 1m test terrain",

                        HistoricalPeriod =
                            "Synthetic Test Data",

                        Author =
                            "Middle_Earth_GIS",

                        Notes =
                            "Generated 1m test terrain dataset."
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

                    ResolutionMeters =
                        ResolutionMeters,

                    Priority =
                        Priority,

                    Version = 1,

                    CoverageBounds =
                        new TerrainBounds(
                            startTileX * TileWidthMeters,
                            startTileY * TileWidthMeters,
                            (startTileX + width) * TileWidthMeters,
                            (startTileY + height) * TileWidthMeters),

                    TileSize = SampleCount,

                    HeightFormat =  TerrainConstants.UInt16HeightFormat,

                    MinElevation = minHeight,

                    MaxElevation = maxHeight,

                    Attributes =
                        new TerrainAttributes
                        {
                            Source =
                                "Procedural 1m test terrain",

                            HistoricalPeriod =
                                "Synthetic Test Data",

                            Author =
                                "Middle_Earth_GIS",

                            Notes =
                                "Generated 1m test terrain dataset."
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