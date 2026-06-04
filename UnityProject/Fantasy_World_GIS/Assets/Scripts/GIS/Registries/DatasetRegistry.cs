using System.Collections.Generic;
using Fantasy_World_GIS.GIS.Core;
using System;

namespace Fantasy_World_GIS.GIS.Registries
{
    public class DatasetRegistry
    {
        private readonly Dictionary<string, GisDataset> datasets = new();
        public int Count => datasets.Count;

        public void Register(GisDataset dataset)
        {
            if (datasets.ContainsKey(dataset.DatasetId))
            {
                throw new System.InvalidOperationException($"Dataset already registered: {dataset.DatasetId}");
            }

            datasets.Add(dataset.DatasetId, dataset);
        }

        public bool TryGetDataset(string datasetId, out GisDataset dataset)
        {
            return datasets.TryGetValue(datasetId, out dataset);
        }

        public IEnumerable<GisDataset> GetAllDatasets()
        {
            return datasets.Values;
        }
    }
}