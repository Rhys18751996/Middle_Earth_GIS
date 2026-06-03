using System;
using System.IO;

namespace Fantasy_World_GIS.Terrain
{
    public static class TerrainChunkSaver
    {
        public static void SaveHeightmap(string path, ushort[] heights)
        {
            byte[] bytes = new byte[heights.Length * 2];

            Buffer.BlockCopy(heights, 0, bytes, 0, bytes.Length);
            File.WriteAllBytes(path, bytes);
        }
    }
}