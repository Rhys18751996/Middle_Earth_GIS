using System.Collections.Generic;
using System.Linq;
using Fantasy_World_GIS.GIS.Core;

namespace Fantasy_World_GIS.GIS.Queries
{
    public static class DatasetQueryService
    {
        public static IEnumerable<GisFeature> GetFeatures(GisDataset dataset)
        {
            return dataset.Features;
        }

        public static IEnumerable<GisFeature> GetFeaturesByType(GisDataset dataset, FeatureType type)
        {
            return dataset.Features.Where(feature => feature.FeatureType == type);
        }

        public static IEnumerable<GisFeature> GetFeaturesByAttribute(GisDataset dataset, string attributeName)
        {
            return dataset.Features
                .Where(feature => feature.Attributes.Any(attribute => attribute.Name == attributeName));
        }
    }
}