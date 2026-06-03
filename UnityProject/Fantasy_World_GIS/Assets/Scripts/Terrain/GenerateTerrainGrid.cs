// using UnityEngine;
// using System;

// namespace Fantasy_World_GIS.Terrain
// {
//     /// <summary>
//     /// Generates a grid of test terrain chunks.
//     /// Used for streaming and traversal testing.
//     /// </summary>
//     public class TerrainGridGenerator_Delete : MonoBehaviour
//     {
//         [SerializeField]
//         private int width = 10;

//         [SerializeField]
//         private int height = 10;

//         private void Start()
//         {
//             GenerateGrid(
//                 width,
//                 height);
//         }

//         public void GenerateGrid(
//             int width,
//             int height)
//         {
//             for (int y = 0; y < height; y++)
//             {
//                 for (int x = 0; x < width; x++)
//                 {
//                     GenerateChunk(
//                         x,
//                         y);
//                 }
//             }

//             Debug.Log(
//                 $"Generated {width * height} chunks");
//         }

//         private void GenerateChunk(int chunkX,int chunkY)
//         {
//             GenerateTestTerrain.GenerateChunk(
//                 chunkX,
//                 chunkY);
//         }
//     }
// }