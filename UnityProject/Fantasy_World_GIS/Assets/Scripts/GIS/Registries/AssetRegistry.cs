using System.Collections.Generic;
using Fantasy_World_GIS.GIS.Core;

namespace Fantasy_World_GIS.GIS.Registries
{
    public class AssetRegistry
    {
        private readonly Dictionary<string, GisAssetDefinition> assets = new();
        public int Count => assets.Count;
        
        public void Register(GisAssetDefinition asset)
        {
            if (assets.ContainsKey(asset.AssetId))
            {
                throw new System.InvalidOperationException($"Asset already registered: {asset.AssetId}");
            }

            assets.Add(asset.AssetId, asset);
        }

        public bool TryGetAsset(string assetId, out GisAssetDefinition asset)
        {
            return assets.TryGetValue(assetId, out asset);
        }
    }
}