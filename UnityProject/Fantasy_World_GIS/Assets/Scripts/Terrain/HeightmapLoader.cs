using System;
using System.IO;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Loads raw UInt16 terrain heightmaps from .bin files.
    /// </summary>
    public static class HeightmapLoader
    {
        public static ushort[] Load(
            string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    $"Heightmap file not found: {path}");
            }

            byte[] bytes =
                File.ReadAllBytes(path);

            if (bytes.Length == 0)
            {
                throw new InvalidDataException(
                    $"Heightmap file is empty: {path}");
            }

            if ((bytes.Length % sizeof(ushort)) != 0)
            {
                throw new InvalidDataException(
                    $"Heightmap file size is invalid: {path}");
            }

            ushort[] heights =
                new ushort[
                    bytes.Length /
                    sizeof(ushort)];

            Buffer.BlockCopy(
                bytes,
                0,
                heights,
                0,
                bytes.Length);

            return heights;
        }
    }
}