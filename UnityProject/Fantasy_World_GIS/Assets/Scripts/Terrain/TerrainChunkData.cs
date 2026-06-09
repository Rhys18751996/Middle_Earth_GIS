using System;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Authoritative terrain chunk data.
    ///
    /// Contains metadata describing a terrain chunk together with
    /// its loaded height samples.
    ///
    /// All terrain chunks occupy a fixed-size world area while
    /// sample density is determined by the owning dataset's
    /// resolution.
    /// </summary>
    [Serializable]
    public class TerrainChunkData
    {
        public string DatasetId = "MiddleEarth_1m_Test";

        public string DatasetType = TerrainConstants.TerrainDatasetType;

        public string ChunkId;

        public int ChunkX;
        public int ChunkY;

        public int SampleCountX;
        public int SampleCountY;

        public float ResolutionMeters;

        public TerrainBounds Bounds;

        public string HeightFormat = TerrainConstants.UInt16HeightFormat;

        public float MinElevation;
        public float MaxElevation;

        public int Version = 1;

        public string HeightMapFile;

        public TerrainAttributes Attributes = new();

        public ushort[] Heights;

        public float ChunkWidthMeters => TerrainConstants.ChunkSizeMeters;
        public float ChunkHeightMeters => TerrainConstants.ChunkSizeMeters;

        public TerrainBounds EffectiveBounds
        {
            get
            {
                if (Bounds != null)
                    return Bounds;

                float width = ChunkWidthMeters;
                float height = ChunkHeightMeters;

                float minX = ChunkX * width;
                float minY = ChunkY * height;

                return new TerrainBounds(minX, minY, minX + width, minY + height);
            }
        }
    }
}