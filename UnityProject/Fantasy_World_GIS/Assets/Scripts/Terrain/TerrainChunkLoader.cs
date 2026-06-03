using System.IO;
using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Loads a terrain chunk from JSON and its associated heightmap file.
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

            string directory = Path.GetDirectoryName(jsonPath);
            string heightmapPath = Path.Combine(directory, chunk.HeightMapFile);
            chunk.Heights = HeightmapLoader.Load(heightmapPath);

            return chunk;
        }
    }
}