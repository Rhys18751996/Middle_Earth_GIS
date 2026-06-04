using System.Collections.Generic;

namespace Fantasy_World_GIS.GIS.Core
{
    public abstract class GisDataset
    {
        public string DatasetId;

        public string Name;

        public string Description;

        public int SchemaVersion;

        public List<GisFeature> Features = new();
    }
}