using UnityEngine;
using System;

namespace Fantasy_World_GIS.Terrain
{
    public class MoveAcrossChunksTest : MonoBehaviour
    {
        [SerializeField]
        private TerrainStreamingSystem streamingSystem;

        [SerializeField]
        private float speed = 50f;

        private void Start()
        {
            transform.position =
                new Vector3(
                    300,
                    0,
                    300);
        }

        private void Update()
        {
            transform.position +=
                Vector3.right *
                speed *
                Time.deltaTime;

            streamingSystem.UpdateStreaming(
                transform.position);
        }
    }
}