using System;
using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    public class TerrainStartupTest : MonoBehaviour
    {
        [SerializeField]
        private TerrainChunkManager chunkManager;

        private void Start()
        {
            chunkManager.LoadChunkRadius(1,1,1);
        }
    }
}