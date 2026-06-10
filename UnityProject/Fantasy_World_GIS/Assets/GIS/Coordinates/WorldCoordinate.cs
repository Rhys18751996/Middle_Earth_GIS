using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy_World_GIS.GIS.Coordinates
{
    public struct WorldCoordinate
    {
        public double X;

        public double Y;

        public WorldCoordinate(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
