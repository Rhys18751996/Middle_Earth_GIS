using System.IO;
using System.Collections.Generic;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Runtime representation of a terrain dataset.
    ///
    /// A dataset corresponds to a single terrain resolution layer
    /// (for example 1m, 4m, 16m or 256m).
    ///
    /// The dataset stores its manifest metadata, filesystem location,
    /// coverage bounds and an in-memory registry of available chunks.
    ///
    /// Chunk existence checks are performed using the AvailableChunks
    /// registry rather than filesystem lookups.
    /// </summary>
    public class TerrainDataset
    {
        public TerrainDatasetManifest Manifest;

        public string FolderPath;

        public HashSet<(int X, int Y)> AvailableChunks = new();
        public string DatasetId => Manifest.DatasetId;

        public float ResolutionMeters => Manifest.ResolutionMeters;

        public int Priority => Manifest.Priority;

        public TerrainBounds CoverageBounds => Manifest.CoverageBounds;

        public bool ContainsChunk(int chunkX, int chunkY)
        {
            return AvailableChunks.Contains((chunkX, chunkY));
        }
    }
}