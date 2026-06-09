namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Resolves which dataset should supply a terrain chunk.
    ///
    /// Datasets are evaluated in priority order and the first
    /// dataset that:
    /// 1. Covers the requested chunk coordinate.
    /// 2. Contains the requested chunk.
    ///
    /// is selected.
    ///
    /// Dataset priority is determined by the
    /// TerrainDatasetRegistry during startup.
    /// </summary>
    public class TerrainDatasetResolver
    {
        private readonly TerrainDatasetRegistry registry;

        public TerrainDatasetResolver(TerrainDatasetRegistry registry)
        {
            this.registry = registry;
        }

        public TerrainDataset ResolveChunk(int chunkX, int chunkY)
        {
            foreach (TerrainDataset dataset in registry.Datasets)
            {
                if (!dataset.CoverageBounds.MayContainChunk(chunkX,chunkY))
                {
                    continue;
                }

                if (!dataset.ContainsChunk(chunkX,chunkY))
                {
                    continue;
                }

                return dataset;
            }

            return null;
        }
    }
}