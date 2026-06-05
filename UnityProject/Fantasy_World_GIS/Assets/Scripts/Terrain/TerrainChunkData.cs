using System;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Authoritative terrain tile metadata plus loaded height samples.
    ///
    /// The legacy Chunk* fields are retained as chunk-grid indexing aliases for
    /// Phase 0 data compatibility, but terrain storage is dataset/tile based.
    /// </summary>
    [Serializable]
    public class TerrainChunkData
    {
        public string DatasetId = "MiddleEarth_1m_Test";
        public string DatasetType = TerrainConstants.TerrainDatasetType;
        public string TileId;

        public int TileX;
        public int TileY;

        public int ChunkX;
        public int ChunkY;

        public int SampleCountX;
        public int SampleCountY;

        public float CellSize;

        public TerrainBounds Bounds;

        public string HeightFormat = TerrainConstants.UInt16HeightFormat;
        public float MinElevation;
        public float MaxElevation;
        public int Version = 1;

        public string HeightMapFile;

        public TerrainAttributes Attributes = new();

        public ushort[] Heights;

        /// <summary>
        /// Backward-compatible identifier used by existing Unity render paths.
        /// </summary>
        public string ChunkId;

        public string EffectiveTileId =>
            !string.IsNullOrWhiteSpace(TileId) ? TileId : ChunkId;

        public int EffectiveTileX => TileX != 0 || ChunkX == 0 ? TileX : ChunkX;
        public int EffectiveTileY => TileY != 0 || ChunkY == 0 ? TileY : ChunkY;

        public TerrainBounds EffectiveBounds
        {
            get
            {
                if (Bounds != null)
                {
                    return Bounds;
                }

                double minX = ChunkX * TerrainConstants.ChunkSizeMeters;
                double minY = ChunkY * TerrainConstants.ChunkSizeMeters;
                double width = (SampleCountX - 1) * CellSize;
                double height = (SampleCountY - 1) * CellSize;

                return new TerrainBounds(minX, minY, minX + width, minY + height);
            }
        }

        public void NormalizeLegacyFields()
        {
            if (string.IsNullOrWhiteSpace(TileId))
            {
                TileId = !string.IsNullOrWhiteSpace(ChunkId)
                    ? ChunkId
                    : $"Tile_{ChunkFileNaming.FormatCoordinatePublic(ChunkX)}_{ChunkFileNaming.FormatCoordinatePublic(ChunkY)}";
            }

            if (string.IsNullOrWhiteSpace(ChunkId))
            {
                ChunkId = TileId;
            }

            TileX = TileX == 0 && ChunkX != 0 ? ChunkX : TileX;
            TileY = TileY == 0 && ChunkY != 0 ? ChunkY : TileY;
            ChunkX = ChunkX == 0 && TileX != 0 ? TileX : ChunkX;
            ChunkY = ChunkY == 0 && TileY != 0 ? TileY : ChunkY;

            if (string.IsNullOrWhiteSpace(DatasetType))
            {
                DatasetType = TerrainConstants.TerrainDatasetType;
            }

            if (string.IsNullOrWhiteSpace(HeightFormat))
            {
                HeightFormat = TerrainConstants.UInt16HeightFormat;
            }

            if (Version <= 0)
            {
                Version = 1;
            }

            Bounds ??= EffectiveBounds;
            Attributes ??= new TerrainAttributes();
        }
    }
}
