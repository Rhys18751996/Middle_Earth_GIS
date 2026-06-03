namespace Fantasy_World_GIS.Terrain
{
    [Serializable]
    public class TerrainChunkData
    {
        public string ChunkId;

        public int ChunkX;
        public int ChunkY;

        public int SampleCountX;
        public int SampleCountY;

        public float CellSize;

        public string HeightMapFile;

        public ushort[] Heights;
    }
}