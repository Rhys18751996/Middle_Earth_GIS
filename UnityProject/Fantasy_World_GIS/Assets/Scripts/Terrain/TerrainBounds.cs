using System;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Axis-aligned world-space bounds for a terrain dataset or tile.
    /// X/Y are horizontal GIS coordinates in meters.
    ///
    /// Unity JsonUtility reliably serializes floats, so the Unity runtime
    /// representation stores bounds as float values while the platform
    /// documentation can still describe exported GIS data at double precision.
    /// </summary>
    [Serializable]
    public class TerrainBounds
    {
        public float MinX;
        public float MinY;
        public float MaxX;
        public float MaxY;

        public TerrainBounds()
        {
        }

        public TerrainBounds(double minX, double minY, double maxX, double maxY)
        {
            MinX = (float)minX;
            MinY = (float)minY;
            MaxX = (float)maxX;
            MaxY = (float)maxY;
        }
    }
}
