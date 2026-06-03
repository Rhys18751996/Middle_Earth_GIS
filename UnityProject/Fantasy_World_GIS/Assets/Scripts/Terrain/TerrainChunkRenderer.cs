using System;
using System.IO;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Loads raw UInt16 terrain heightmaps from .bin files.
    /// </summary>
    public static class HeightmapLoader
    {
        public static ushort[] Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    $"Heightmap file not found: {path}");
            }

            byte[] bytes = File.ReadAllBytes(path);

            ushort[] heights = new ushort[bytes.Length / 2];

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