using System.Linq;

namespace Fantasy_World_GIS.Terrain
{
    public class TerrainDatasetResolver
    {
        private readonly TerrainDatasetRegistry registry;

        public TerrainDatasetResolver(TerrainDatasetRegistry registry)
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
                if (!ContainsChunk(
                        dataset,
                        chunkX,
                        chunkY))
                {
                    continue;
                }

                if (bestDataset == null ||
                    dataset.Priority >
                    bestDataset.Priority)
                {
                    bestDataset =
                        dataset;
                }
            }

            return bestDataset;
        }

        private bool ContainsChunk(
            TerrainDataset dataset,
            int chunkX,
            int chunkY)
        {
            TerrainBounds bounds =
                dataset.Manifest.CoverageBounds;

            double minX =
                chunkX *
                TerrainConstants.ChunkSizeMeters;

            double minY =
                chunkY *
                TerrainConstants.ChunkSizeMeters;

            double maxX =
                minX +
                TerrainConstants.ChunkSizeMeters;

            double maxY =
                minY +
                TerrainConstants.ChunkSizeMeters;

            return
                minX >= bounds.MinX &&
                minY >= bounds.MinY &&
                maxX <= bounds.MaxX &&
                maxY <= bounds.MaxY;
        }
    }
}