using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
    public class PlayerCarMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerCarComponent, PlayerInputData>> _filter = default;
        private readonly EcsPoolInject<PlayerCarComponent> _carPool = default;
        private readonly EcsPoolInject<WheelComponent> _wheelsPool = default;
        private readonly EcsPoolInject<PlayerInputData> _inputPool = default;

        private float _speed;
        private float _brake;
        private bool _handbrake;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var player = ref _carPool.Value.Get(i);
                ref var input = ref _inputPool.Value.Get(i);
                ref var wheels = ref _wheelsPool.Value.Get(i);

                UpdateSpeed(ref player);
                UpdateBrake(ref player, ref input);
                UpdateHandbrake(ref input);

                ApplyBrake(ref wheels, ref player);
                ApplyMotor(ref wheels, ref player, ref input);
                ApplyHandbrake(ref wheels);
                ApplySteering(ref wheels, ref input, ref player, _speed);
                ApplyMeshWheelPosition(ref wheels);
            }
        }


        private void UpdateSpeed(ref PlayerCarComponent player)
        {
            _speed = player.PlayerRigidbody.linearVelocity.magnitude;
        }

        private void UpdateBrake(ref PlayerCarComponent player, ref PlayerInputData input)
        {
            float movingDirection = Vector3.Dot(player.PlayerTransform.forward, player.PlayerRigidbody.linearVelocity);
            if (movingDirection < -0.5f && input.GasInput > 0)
            {
                _brake = Mathf.Abs(input.GasInput);
            }
            else if (movingDirection > 0.5f && input.GasInput < 0)
            {
                _brake = Mathf.Abs(input.GasInput);
            }
            else
            {
                _brake = 0;
            }
        }

        private void UpdateHandbrake(ref PlayerInputData input)
        {
            _handbrake = input.HandBrakeInput;
        }

        private void ApplyBrake(ref WheelComponent wheels, ref PlayerCarComponent player)
        {
            wheels.FrWheelCollider.brakeTorque = _brake * player.BrakePower * 0.7f;
            wheels.FlWheelCollider.brakeTorque = _brake * player.BrakePower * 0.7f;
            wheels.RrWheelCollider.brakeTorque = _brake * player.BrakePower * 0.3f;
            wheels.RlWheelCollider.brakeTorque = _brake * player.BrakePower * 0.3f;
        }

        private void ApplyHandbrake(ref WheelComponent wheels)
        {
            if (_handbrake)
            {
                wheels.RrWheelCollider.brakeTorque = 20000;
                wheels.RlWheelCollider.brakeTorque = 20000;
            }
            else
            {
                wheels.RrWheelCollider.brakeTorque = 0;
                wheels.RlWheelCollider.brakeTorque = 0;
            }
        }


        private static void ApplySteering(ref WheelComponent wheels, ref PlayerInputData input,
            ref PlayerCarComponent player, float speed)
        {
            float steeringAngle = input.SteeringInput * player.SteeringCurve.Evaluate(speed);
            wheels.FrWheelCollider.steerAngle = steeringAngle;
            wheels.FlWheelCollider.steerAngle = steeringAngle;
        }

        private static void ApplyMotor(ref WheelComponent wheels, ref PlayerCarComponent player,
            ref PlayerInputData input)
        {
            wheels.RrWheelCollider.motorTorque = player.MotorPower * input.GasInput;
            wheels.RlWheelCollider.motorTorque = player.MotorPower * input.GasInput;
        }

        private void ApplyMeshWheelPosition(ref WheelComponent wheels)
        {
            UpdateWheelMeshPosition(wheels.FlWheelCollider, wheels.FlWheelMesh);
            UpdateWheelMeshPosition(wheels.FrWheelCollider, wheels.FrWheelMesh);
            UpdateWheelMeshPosition(wheels.RrWheelCollider, wheels.RrWheelMesh);
            UpdateWheelMeshPosition(wheels.RlWheelCollider, wheels.RlWheelMesh);
        }

        private void UpdateWheelMeshPosition(WheelCollider collider, MeshRenderer wheelMesh)
        {
            collider.GetWorldPose(out Vector3 position, out Quaternion quaternion);
            wheelMesh.transform.position = position;
            wheelMesh.transform.rotation = quaternion;
        }
    }
}