using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicahW.PointGrass {
    public class ObjectOscillator : MonoBehaviour {
        public Vector3 oscStrength = Vector3.right;
        public Vector3 oscFrequency = Vector3.right;
        public Vector3 oscPhase = Vector3.zero;
        public Vector3 oscOffset = Vector3.zero;

        private Vector3 origin;

        private void Start() {
            origin = transform.position;
        }

        private void Update() {
            float frequencyMul = Time.time * Mathf.PI * 2f;
            Vector3 phase = oscPhase * Mathf.PI * 2f;
            // Calculate the offset
            Vector3 offset = new Vector3(
                Mathf.Sin(oscFrequency.x * frequencyMul + phase.x),
                Mathf.Sin(oscFrequency.y * frequencyMul + phase.y),
                Mathf.Sin(oscFrequency.z * frequencyMul + phase.z)
            );
            offset.Scale(oscStrength);
            offset += oscOffset;
            // Update the object's position
            transform.position = origin + offset;
        }
    }
}