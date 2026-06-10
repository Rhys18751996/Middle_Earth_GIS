using System;
using System.Collections.Generic;
using System.Linq;

using Fantasy_World_GIS.GIS.Coordinates;
using Fantasy_World_GIS.GIS.Core;
using Fantasy_World_GIS.GIS.Geometry;

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

        public static IEnumerable<GisFeature> GetFeaturesNearPoint(GisDataset dataset, WorldCoordinate point, double distance)
        {
            foreach (GisFeature feature in dataset.Features)
            {
                if (feature.Geometry is not PointGeometry pointGeometry)
                {
                    continue;
                }

                double dx = pointGeometry.Coordinate.X - point.X;
                double dy = pointGeometry.Coordinate.Y - point.Y;
                double distanceToFeature = Math.Sqrt(dx * dx + dy * dy);

                if (distanceToFeature <= distance)
                {
                    yield return feature;
                }
            }
        }
    }
}