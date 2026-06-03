using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Creates a GameObject representation of a terrain chunk.
    /// </summary>
    public static class TerrainChunkRenderer
    {
        public static GameObject CreateChunk(
            TerrainChunkData chunk,
            Material material = null)
        {
            Mesh mesh =
                TerrainMeshGenerator.GenerateMesh(chunk);

            GameObject chunkObject =
                new GameObject(chunk.ChunkId);

            MeshFilter meshFilter =
                chunkObject.AddComponent<MeshFilter>();

            MeshRenderer meshRenderer =
                chunkObject.AddComponent<MeshRenderer>();

            MeshCollider meshCollider =
                chunkObject.AddComponent<MeshCollider>();

            meshFilter.sharedMesh = mesh;
            meshCollider.sharedMesh = mesh;

            if (material != null)
            {
                meshRenderer.material = material;
            }

            chunkObject.transform.position = new Vector3(chunk.ChunkX * 256, 0, chunk.ChunkY * 256);
                    
            return chunkObject;
        }
    }
}