using System;
using System.IO;
using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    public class GenerateTestTerrain : MonoBehaviour
    {
        private void Start()
        {
            string path =
                Application.dataPath +
                "/Data/Terrain/terrain_000_000.bin";

            ushort[] heights = new ushort[257 * 257];

            for (int y = 0; y < 257; y++)
            {
                for (int x = 0; x < 257; x++)
                {
                    int index = y * 257 + x;

                    float dx = x - 128;
                    float dy = y - 128;

                    float distance =
                        Mathf.Sqrt(dx * dx + dy * dy);

                    // float height =
                    //     Mathf.Max(0, 100 - distance);
                    float height = 100f * Mathf.Exp(-(distance * distance) / 5000f);
                    
                    heights[index] = (ushort)height;
                }
            }

            byte[] bytes =
                new byte[heights.Length * 2];

            Buffer.BlockCopy(
                heights,
                0,
                bytes,
                0,
                bytes.Length);

            File.WriteAllBytes(path, bytes);

            Debug.Log(
                $"Created {path}");
        }
    }
}