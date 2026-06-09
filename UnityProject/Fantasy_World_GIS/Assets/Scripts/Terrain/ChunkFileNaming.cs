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

        public static bool TryParseCoordinates(
    string fileName,
    out int chunkX,
    out int chunkY)
        {
            chunkX = 0;
            chunkY = 0;

            string[] parts =
                fileName.Split('_');

            if (parts.Length != 3)
            {
                return false;
            }

            return
                TryParseCoordinate(parts[1], out chunkX) &&
                TryParseCoordinate(parts[2], out chunkY);
        }

        private static bool TryParseCoordinate(
            string value,
            out int result)
        {
            result = 0;

            if (string.IsNullOrWhiteSpace(value) ||
                value.Length < 2)
            {
                return false;
            }

            char sign =
                value[0];

            if (!int.TryParse(
                    value.Substring(1),
                    out int magnitude))
            {
                return false;
            }

            result =
                sign == 'N'
                    ? -magnitude
                    : magnitude;

            return true;
        }
    }
}