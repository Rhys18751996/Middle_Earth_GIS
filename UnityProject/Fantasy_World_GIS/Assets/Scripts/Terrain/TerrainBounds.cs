using System;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Axis-aligned world-space bounds for a terrain dataset or tile.
    /// X/Y are horizontal GIS coordinates in meters.
    /// </summary>
    [Serializable]
    public class TerrainBounds
    {
        public double MinX;
        public double MinY;
        public double MaxX;
        public double MaxY;

        public TerrainBounds()
        {
        }

        public TerrainBounds(double minX, double minY, double maxX, double maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }
    }
}
