using System.Collections.Generic;

namespace Fantasy_World_GIS.GIS.Core
{
    public abstract class GisFeature
    {
        public string FeatureId;

        public FeatureType FeatureType;

        public List<GisAttribute> Attributes { get; } = new();
    }
}