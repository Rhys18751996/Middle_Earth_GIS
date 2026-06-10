using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fantasy_World_GIS.GIS.Coordinates;

namespace Fantasy_World_GIS.GIS.Geometry
{
    public struct GisBounds
    {
        public double MinX;
        public double MinY;
        public double MaxX;
        public double MaxY;

        public GisBounds(double minX, double minY, double maxX, double maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }

        public bool Contains(WorldCoordinate coordinate)
        {
            return
                coordinate.X >= MinX &&
                coordinate.X <= MaxX &&
                coordinate.Y >= MinY &&
                coordinate.Y <= MaxY;
        }
    }
}
