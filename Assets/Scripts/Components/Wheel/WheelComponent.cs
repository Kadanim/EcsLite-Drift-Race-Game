using UnityEngine;

namespace Components.Wheel
{
    public struct WheelComponent
    {
        public WheelCollider FrWheelCollider;
        public WheelCollider FlWheelCollider;
        public WheelCollider RrWheelCollider;
        public WheelCollider RlWheelCollider;

        public MeshRenderer FrWheelMesh;
        public MeshRenderer FlWheelMesh;
        public MeshRenderer RrWheelMesh;
        public MeshRenderer RlWheelMesh;
    }
}