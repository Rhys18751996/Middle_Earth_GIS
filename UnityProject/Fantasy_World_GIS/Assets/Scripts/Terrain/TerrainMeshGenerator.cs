using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Generates Unity meshes from terrain height data.
    ///
    /// All terrain chunks occupy the same world-space footprint.
    /// Dataset resolution affects only sample density and mesh
    /// detail, not chunk dimensions.
    ///
    /// Produces vertices, triangles, UVs and mesh bounds from
    /// terrain height samples.
    /// </summary>
    public static class TerrainMeshGenerator
    {
        private const float ChunkSizeMeters = 256f;

        public static Mesh GenerateMesh(TerrainChunkData chunk)
        {
            int width = chunk.SampleCountX;
            int height = chunk.SampleCountY;

            Vector3[] vertices = new Vector3[width * height];
            Vector2[] uvs = new Vector2[width * height];
            int[] triangles = new int[(width - 1) * (height - 1) * 6];

            GenerateVertices(chunk, vertices, uvs);
            GenerateTriangles(width, height, triangles);

            Mesh mesh = new Mesh();

            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            mesh.name = chunk.ChunkId;
            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }

        private static void GenerateVertices(TerrainChunkData chunk, Vector3[] vertices, Vector2[] uvs)
        {
            int width = chunk.SampleCountX;
            int height = chunk.SampleCountY;

            float sampleSpacingX = ChunkSizeMeters / (width - 1);
            float sampleSpacingY = ChunkSizeMeters / (height - 1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;

                    float elevation = chunk.Heights[index];

                    vertices[index] =
                        new Vector3(
                            x * sampleSpacingX,
                            elevation,
                            y * sampleSpacingY);

                    uvs[index] =
                        new Vector2(
                            (float)x / (width - 1),
                            (float)y / (height - 1));
                }
            }
        }

        private static void GenerateTriangles(int width, int height, int[] triangles)
        {
            int triangleIndex = 0;

            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    int bottomLeft = y * width + x;
                    int bottomRight = bottomLeft + 1;

                    int topLeft = bottomLeft + width;
                    int topRight = topLeft + 1;

                    triangles[triangleIndex++] = bottomLeft;
                    triangles[triangleIndex++] = topLeft;
                    triangles[triangleIndex++] = topRight;
                    triangles[triangleIndex++] = bottomLeft;
                    triangles[triangleIndex++] = topRight;
                    triangles[triangleIndex++] = bottomRight;
                }
            }
        }
    }
}