using System.Collections.Generic;
using Fantasy_World_GIS.GIS.Core;

namespace Fantasy_World_GIS.GIS.Registries
{
    public class FeatureRegistry
    {
        private readonly Dictionary<string, GisFeature> features = new();
        public int Count => features.Count;
        
        public void Register(GisFeature feature)
        {
            if (features.ContainsKey(feature.FeatureId))
            {
                throw new System.InvalidOperationException($"Feature already registered: {feature.FeatureId}");
            }

            features.Add(feature.FeatureId, feature);
        }

        public bool TryGetFeature(string featureId, out GisFeature feature)
        {
            return features.TryGetValue(featureId, out feature);
        }

        public IEnumerable<GisFeature> GetAllFeatures()
        {
            return features.Values;
        }
    }
}