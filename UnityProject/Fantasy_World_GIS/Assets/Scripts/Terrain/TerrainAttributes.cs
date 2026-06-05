using System;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Serializable metadata attached to terrain tiles.
    /// Kept explicit so Unity JsonUtility can read and write it.
    /// </summary>
    [Serializable]
    public class TerrainAttributes
    {
        public string Source = "Generated";
        public string HistoricalPeriod = "Unspecified";
        public string Author = "Middle_Earth_GIS";
        public string Notes = string.Empty;
    }
}
