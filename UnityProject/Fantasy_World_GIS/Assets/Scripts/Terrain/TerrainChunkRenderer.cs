using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
/// <summary>
/// Creates Unity GameObjects for terrain chunks.
///
/// Converts loaded terrain data into scene objects by
/// generating meshes, assigning materials and positioning
/// the resulting GameObjects in world space.
/// </summary>
    public static class TerrainChunkRenderer
    {
        public static GameObject CreateChunk(TerrainChunkData chunk, Material material = null)
        {
            Mesh mesh = TerrainMeshGenerator.GenerateMesh(chunk);

            GameObject chunkObject = new GameObject($"{chunk.DatasetId}_{chunk.ChunkId}");

            MeshFilter meshFilter = chunkObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = chunkObject.AddComponent<MeshRenderer>();
            MeshCollider meshCollider = chunkObject.AddComponent<MeshCollider>();

            meshFilter.sharedMesh = mesh;
            meshCollider.sharedMesh = mesh;

            if (material != null)
            {
                meshRenderer.material = material;

                // Debug colours
                if (chunk.DatasetId == "MiddleEarth_1m_Test")
                {
                    meshRenderer.material.color = Color.green;
                }
                else if (chunk.DatasetId == "MiddleEarth_256m_Test")
                {
                    meshRenderer.material.color = Color.red;
                }
            }

            TerrainBounds bounds = chunk.EffectiveBounds;
            chunkObject.transform.position = new Vector3(bounds.MinX, 0f, bounds.MinY);

            return chunkObject;
        }
    }
}