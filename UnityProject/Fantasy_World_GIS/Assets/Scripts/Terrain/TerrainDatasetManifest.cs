using System;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Dataset-level description for an authoritative terrain resolution layer.
    /// Terrain tiles reference this manifest by DatasetId.
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