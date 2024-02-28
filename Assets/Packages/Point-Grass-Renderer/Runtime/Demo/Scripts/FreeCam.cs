using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicahW.PointGrass {
    public class FreeCam : MonoBehaviour {
        public float lookSpeed = 7.5f;
        public float moveSpeed = 5f;
        public float moveAccel = 30f;
        public float fastSpeed = 15f;

        float currentPitch;
        float currentYaw;
        Vector3 currentVelocity;

        void Start() {
#if !ENABLE_LEGACY_INPUT_MANAGER
            Debug.LogWarning($"Camera \"{gameObject.name}\" is not moveable since the legacy input manager is disabled");
#endif
            currentPitch = transform.rotation.eulerAngles.x;
            currentYaw = transform.rotation.eulerAngles.y;

            currentVelocity = Vector3.zero;
        }

        void Update() {
#if ENABLE_LEGACY_INPUT_MANAGER
            // Get the mouse input
            Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            // Get movement input
            Vector3 moveInput = new Vector3(
                Input.GetAxis("Horizontal"),
                (Input.GetKey(KeyCode.E) ? 1f : 0f) + (Input.GetKey(KeyCode.Q) ? -1f : 0f),
                Input.GetAxis("Vertical")
            );
            // Calculate the target speed
            float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : moveSpeed;

            // Update the current pitch and yaw
            currentPitch = (currentPitch - lookSpeed * mouseInput.y) % 360f;
            currentYaw = (currentYaw + lookSpeed * mouseInput.x) % 360f;
            // Update the camera's rotation
            transform.rotation = Quaternion.AngleAxis(currentYaw, Vector3.up) * Quaternion.AngleAxis(currentPitch, Vector3.right);
            // Calculate the current velocity
            Vector3 targetVelocity = transform.TransformDirection(moveInput) * targetSpeed;
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, moveAccel * Time.deltaTime);
            // Update the camera's position
            transform.position += currentVelocity * Time.deltaTime;
#endif
        }
    }
}