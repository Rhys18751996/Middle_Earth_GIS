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

            ushort[] heights =
                new ushort[257 * 257];

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