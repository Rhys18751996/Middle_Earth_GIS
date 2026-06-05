namespace Fantasy_World_GIS.Terrain
{
    public class TerrainDataset
    {
        public TerrainDatasetManifest Manifest;

        public string FolderPath;

        public string DatasetId => Manifest.DatasetId;

        public float CellSize => Manifest.CellSize;

        public int Priority =>  Manifest.Priority;
    }
}