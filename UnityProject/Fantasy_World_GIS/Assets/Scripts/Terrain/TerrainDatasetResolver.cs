namespace Fantasy_World_GIS.Terrain
{
    public class TerrainDatasetResolver
    {
        private readonly TerrainDatasetRegistry registry;

        public TerrainDatasetResolver(
            TerrainDatasetRegistry registry)
        {
            this.registry = registry;
        }

        public TerrainDataset ResolveChunk(
            int chunkX,
            int chunkY)
        {
            TerrainDataset bestDataset = null;

            foreach (TerrainDataset dataset in registry.Datasets)
            {
                if (!dataset.ContainsChunkFile(
                        chunkX,
                        chunkY))
                {
                    continue;
                }

                if (bestDataset == null ||
                    dataset.Priority > bestDataset.Priority)
                {
                    bestDataset = dataset;
                }
            }

            return bestDataset;
        }
    }
}