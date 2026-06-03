namespace Fantasy_World_GIS.Terrain
{
    public static class ChunkFileNaming
    {
        public static string GetChunkFileName(
            int chunkX,
            int chunkY)
        {
            return
                $"Chunk_{FormatCoordinate(chunkX)}_" +
                $"{FormatCoordinate(chunkY)}.json";
        }

        private static string FormatCoordinate(
            int value)
        {
            string prefix =
                value >= 0 ? "P" : "N";

            return
                $"{prefix}{System.Math.Abs(value):000}";
        }
    }
}