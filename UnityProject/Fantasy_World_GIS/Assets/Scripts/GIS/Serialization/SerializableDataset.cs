using System.Collections.Generic;
using Fantasy_World_GIS.GIS.Core;
using System; 

namespace Fantasy_World_GIS.GIS.Serialization
{
    [Serializable]
    public class SerializableDataset
    {
        public string DatasetId;

        public string Name;

        public string Description;

        public int SchemaVersion;
    }
}