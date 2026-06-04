using System.Collections.Generic;
using Fantasy_World_GIS.GIS.Core;

namespace Fantasy_World_GIS.GIS.Serialization
{
    [System.Serializable]
    public class SerializableDataset
    {
        public string DatasetId;

        public string Name;

        public string Description;

        public int SchemaVersion;
    }
}