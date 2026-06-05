namespace Fantasy_World_GIS.Terrain
{
    public static class ChunkFileNaming
    {
        public static string GetChunkFileName(int chunkX, int chunkY)
        {
            return $"Chunk_{FormatCoordinate(chunkX)}_" + $"{FormatCoordinate(chunkY)}.json";
        }

        public static string FormatCoordinatePublic(int value)
        {
            string prefix = value >= 0 ? "P" : "N";
            return $"{prefix}{System.Math.Abs(value):000}";
        }

        private static string FormatCoordinate(int value)
        {
            return FormatCoordinatePublic(value);
        }
        
        public static string GetHeightmapFileName(int chunkX, int chunkY)
        {
            return $"terrain_{FormatCoordinate(chunkX)}_" + $"{FormatCoordinate(chunkY)}.bin";
        }
    }
}