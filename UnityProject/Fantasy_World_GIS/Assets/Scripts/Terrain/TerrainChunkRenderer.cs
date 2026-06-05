using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Creates a GameObject representation of a terrain tile.
    /// </summary>
    public static class TerrainChunkRenderer
    {
        public static GameObject CreateChunk(TerrainChunkData chunk, Material material = null)
        {
            Mesh mesh = TerrainMeshGenerator.GenerateMesh(chunk);

            GameObject chunkObject = new GameObject($"{chunk.DatasetId}_{chunk.EffectiveTileId}");

            //Debug.Log($"Render {chunk.EffectiveTileId} using {chunk.HeightMapFile}");

            MeshFilter meshFilter = chunkObject.AddComponent<MeshFilter>();

            MeshRenderer meshRenderer = chunkObject.AddComponent<MeshRenderer>();

            MeshCollider meshCollider = chunkObject.AddComponent<MeshCollider>();

            meshFilter.sharedMesh = mesh;
            meshCollider.sharedMesh = mesh;

            if (material != null)
            {
                meshRenderer.material = material;
            }

            // colour meshes for debugging
            if (chunk.DatasetId == "MiddleEarth_1m_Test")
            {
                meshRenderer.material.color = Color.green;
            }
            else if (chunk.DatasetId == "MiddleEarth_100m_Test")
            {
                meshRenderer.material.color = Color.red;
            }
            TerrainBounds bounds = chunk.EffectiveBounds;
            chunkObject.transform.position = new Vector3((float)bounds.MinX, 0, (float)bounds.MinY);
                    
            return chunkObject;
        }
    }
}