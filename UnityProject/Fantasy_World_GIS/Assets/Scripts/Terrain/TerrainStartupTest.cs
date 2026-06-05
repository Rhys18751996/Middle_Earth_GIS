using UnityEngine;

namespace Fantasy_World_GIS.Terrain
{
    public class TerrainStartupTest : MonoBehaviour
    {
        [SerializeField]
        private TerrainStreamingSystem streamingSystem;

        [SerializeField]
        private Vector3 initialWorldPosition = new(
            384,
            0,
            384);

        private void Start()
        {
            if (streamingSystem == null)
            {
                streamingSystem = FindFirstObjectByType<TerrainStreamingSystem>();
            }

            if (streamingSystem == null)
            {
                Debug.LogWarning("Terrain startup could not find a TerrainStreamingSystem.");
                return;
            }

            streamingSystem.UpdateStreaming(initialWorldPosition);
        }
    }
}
