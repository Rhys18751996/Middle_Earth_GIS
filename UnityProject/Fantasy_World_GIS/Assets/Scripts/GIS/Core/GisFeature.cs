using System.Collections.Generic;

using Fantasy_World_GIS.GIS.Geometry;

namespace Fantasy_World_GIS.GIS.Core
{
    public abstract class GisFeature
    {
        public string FeatureId;

        public FeatureType FeatureType;

        public GisGeometry Geometry;

        public List<GisAttribute> Attributes { get; } = new();
    }
}