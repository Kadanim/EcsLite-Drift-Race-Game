using UnityEngine;

namespace Components.Player
{
    public struct PlayerCarComponent
    {
        public Transform PlayerTransform;
        public Rigidbody PlayerRigidbody;

        public float MotorPower;
        public float BrakePower;
        public AnimationCurve SteeringCurve;
    }
}