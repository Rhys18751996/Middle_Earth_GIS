using UnityEngine;
using System;
using Fantasy_World_GIS.Terrain;

namespace Fantasy_World_GIS.Debugging
{
    public class FlyCameraController : MonoBehaviour
    {
        [SerializeField]
        private TerrainStreamingSystem streamingSystem;

        [SerializeField]
        private float moveSpeed = 150f;

        [SerializeField]
        private float fastMoveSpeed = 600f;

        [SerializeField]
        private float rollSpeed = 90f;

        [SerializeField]
        private float mouseSensitivity = 1f;

        private float yaw;
        private float pitch;

        private void Start()
        {
            Cursor.lockState =
                CursorLockMode.Locked;

            Cursor.visible = false;

            Vector3 rotation =
                transform.eulerAngles;

            yaw = rotation.y;
            pitch = rotation.x;

            if (streamingSystem != null)
            {
                streamingSystem.UpdateStreaming(transform.position);
            }
        }

        private void Update()
        {
            UpdateLook();
            UpdateRoll();
            UpdateMovement();

            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.position =
                    new Vector3(
                        384,
                        450,
                        384);
            }

            streamingSystem.UpdateStreaming(transform.position);
        }

        private void UpdateLook()
        {
            float mouseX =
                Input.GetAxis("Mouse X");

            float mouseY =
                Input.GetAxis("Mouse Y");

            yaw +=
                mouseX * mouseSensitivity;

            pitch -=
                mouseY * mouseSensitivity;

            pitch =
                Mathf.Clamp(
                    pitch,
                    -89f,
                    89f);

            transform.rotation =
                Quaternion.Euler(
                    pitch,
                    yaw,
                    0);
        }

        private void UpdateMovement()
        {
            float speed =
                Input.GetKey(KeyCode.LeftShift)
                    ? fastMoveSpeed
                    : moveSpeed;

            Vector3 movement =
                Vector3.zero;

            if (Input.GetKey(KeyCode.W))
                movement += transform.forward;

            if (Input.GetKey(KeyCode.S))
                movement -= transform.forward;

            if (Input.GetKey(KeyCode.A))
                movement -= transform.right;

            if (Input.GetKey(KeyCode.D))
                movement += transform.right;

            if (Input.GetKey(KeyCode.Space))
                movement += Vector3.up;

            if (Input.GetKey(KeyCode.LeftControl))
                movement += Vector3.down;

            transform.position +=
                movement.normalized *
                speed *
                Time.deltaTime;
        }

        private void UpdateRoll()
        {
            float roll = 0f;

            if (Input.GetKey(KeyCode.Q))
                roll += rollSpeed;

            if (Input.GetKey(KeyCode.E))
                roll -= rollSpeed;

            transform.Rotate(
                0,
                0,
                roll * Time.deltaTime,
                Space.Self);
        }
    }
}