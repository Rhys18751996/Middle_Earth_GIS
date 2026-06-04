namespace Fantasy_World_GIS.GIS.Core
{
    public abstract class GisFeature
    {
        public string FeatureId;

        public string FeatureType;

        public List<GisAttribute> Attributes { get; } = new();
    }
}