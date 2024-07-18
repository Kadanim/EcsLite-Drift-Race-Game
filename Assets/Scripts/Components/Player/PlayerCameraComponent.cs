using UnityEngine;

namespace Components.Player
{
    public struct PlayerCameraComponent
    {
        public Transform CameraTransform;
        public Transform PlayerTransform;
        public Rigidbody PlayerRigidbody;
        public Vector3 Offset;
        public float CameraDistance;
        public float CameraSmooth;
    }
}