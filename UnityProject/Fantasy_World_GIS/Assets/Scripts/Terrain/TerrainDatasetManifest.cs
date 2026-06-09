using System;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Serializable description of a terrain dataset.
    ///
    /// The manifest defines dataset-wide properties such as
    /// resolution, priority, coverage bounds, height format
    /// and elevation limits.
    ///
    /// Manifests are loaded at startup and used to construct
    /// TerrainDataset instances.
    /// </summary>
    [Serializable]
    public class TerrainDatasetManifest
    {
        public string DatasetId;
        public string DatasetType = TerrainConstants.TerrainDatasetType;
        public float ResolutionMeters;
        public int Priority;
        public int Version;
        public TerrainBounds CoverageBounds;
        public int TileSize;
        public string HeightFormat = TerrainConstants.UInt16HeightFormat;
        public float MinElevation;
        public float MaxElevation;
        public TerrainAttributes Attributes = new();
    }
}