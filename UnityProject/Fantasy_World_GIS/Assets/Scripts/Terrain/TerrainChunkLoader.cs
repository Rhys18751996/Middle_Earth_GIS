using System.IO;
using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Loads a terrain tile from JSON and its associated heightmap file.
    /// </summary>
    public static class TerrainChunkLoader
    {
        public static TerrainChunkData Load(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"Chunk file not found: {jsonPath}");
            }
            
            string json = File.ReadAllText(jsonPath);
            TerrainChunkData chunk = JsonUtility.FromJson<TerrainChunkData>(json);
            chunk.NormalizeLegacyFields();

            string directory = Path.GetDirectoryName(jsonPath);
            string heightmapPath = Path.Combine(directory, chunk.HeightMapFile);
            chunk.Heights = HeightmapLoader.Load(heightmapPath);

            Validate(chunk, jsonPath, heightmapPath);

            return chunk;
        }

        private static void Validate(TerrainChunkData chunk, string jsonPath, string heightmapPath)
        {
            if (chunk.DatasetType != TerrainConstants.TerrainDatasetType)
            {
                throw new InvalidDataException(
                    $"Unsupported dataset type '{chunk.DatasetType}' in {jsonPath}.");
            }

            if (chunk.SampleCountX <= 1 || chunk.SampleCountY <= 1)
            {
                throw new InvalidDataException($"Terrain tile has invalid sample dimensions: {jsonPath}");
            }

            int expectedHeightCount = chunk.SampleCountX * chunk.SampleCountY;
            if (chunk.Heights.Length != expectedHeightCount)
            {
                throw new InvalidDataException(
                    $"Heightmap sample count mismatch for {heightmapPath}. " +
                    $"Expected {expectedHeightCount}, found {chunk.Heights.Length}.");
            }

            if (chunk.HeightFormat != TerrainConstants.UInt16HeightFormat)
            {
                throw new InvalidDataException(
                    $"Unsupported height format '{chunk.HeightFormat}' in {jsonPath}.");
            }
        }
    }
}