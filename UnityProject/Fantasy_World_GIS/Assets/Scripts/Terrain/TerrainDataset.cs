using System.IO;

namespace Fantasy_World_GIS.Terrain
{
    public class TerrainDataset
    {
        public TerrainDatasetManifest Manifest;

        public string FolderPath;

        public string DatasetId =>
            Manifest.DatasetId;

        public float ResolutionMeters =>
            Manifest.ResolutionMeters;

        public int Priority =>
            Manifest.Priority;

        public TerrainBounds CoverageBounds =>
            Manifest.CoverageBounds;

        public bool ContainsChunkFile(
            int chunkX,
            int chunkY)
        {
            string chunkPath =
                Path.Combine(
                    FolderPath,
                    ChunkFileNaming.GetChunkFileName(
                        chunkX,
                        chunkY));

            return File.Exists(
                chunkPath);
        }

        //public int DatasetLevel => Manifest.DatasetLevel;
    }
}