using UnityEngine;
using System;

namespace Fantasy_World_GIS.Terrain
{
    public class TerrainStartupTest : MonoBehaviour
    {
        [SerializeField]
        private TerrainStreamingSystem
            streamingSystem;

        private void Start()
        {
            streamingSystem.UpdateStreaming(
                new Vector3(
                    300,
                    0,
                    300));
        }
    }
}
