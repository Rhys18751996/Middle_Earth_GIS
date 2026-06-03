// using UnityEngine;
// using System;

// namespace Fantasy_World_GIS.Terrain
// {
//     public class MoveAcrossChunksTest : MonoBehaviour
//     {
//         [SerializeField]
//         private TerrainStreamingSystem streamingSystem;

//         [SerializeField]
//         private float speed = 500f;

//         private void Start()
//         {
//             transform.position =
//                 new Vector3(
//                     300,
//                     0,
//                     300);
//         }

//         private void Update()
//         {
//             float horizontal =
//                 Input.GetAxisRaw("Horizontal");

//             float vertical =
//                 Input.GetAxisRaw("Vertical");

//             Vector3 movement =
//                 new Vector3(
//                     horizontal,
//                     0,
//                     vertical);

//             transform.position +=
//                 movement.normalized *
//                 speed *
//                 Time.deltaTime;

//             streamingSystem.UpdateStreaming(
//                 transform.position);
//         }
//     }
// }