using Components.Player;
using Components.Wheel;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems.Wheel
{
    public class WheelEffectsUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<WheelSmokeComponent, GroundCheckComponent>> _filter = default;
        private readonly EcsPoolInject<PlayerCarComponent> _carPool = default;
        private readonly EcsPoolInject<WheelSmokeComponent> _smokePool = default;
        private readonly EcsPoolInject<WheelTrailComponent> _trailPool = default;
        private readonly EcsPoolInject<PlayerInputData> _inputPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var groundCheck = ref _filter.Pools.Inc2.Get(i);

                ref var player = ref _carPool.Value.Get(i);
                ref var input = ref _inputPool.Value.Get(i);
                ref var smoke = ref _smokePool.Value.Get(i);
                ref var trail = ref _trailPool.Value.Get(i);

                float slip = Vector3.Dot(player.PlayerRigidbody.linearVelocity.normalized,
                    player.PlayerTransform.forward);
                bool isDrifting = Mathf.Abs(slip) < 0.95f;
                bool isSkidding = input.GasInput > 0 && player.PlayerRigidbody.linearVelocity.magnitude < 5;

                UpdateWheelTrail(smoke.FrWheelSmoke, trail.FrWheelTrail, isDrifting, isSkidding,
                    groundCheck.FrWheelIsGrounded);
                UpdateWheelTrail(smoke.FlWheelSmoke, trail.FlWheelTrail, isDrifting, isSkidding,
                    groundCheck.FlWheelIsGrounded);
                UpdateWheelTrail(smoke.RrWheelSmoke, trail.RrWheelTrail, isDrifting, isSkidding,
                    groundCheck.RrWheelIsGrounded);
                UpdateWheelTrail(smoke.RlWheelSmoke, trail.RlWheelTrail, isDrifting, isSkidding,
                    groundCheck.RlWheelIsGrounded);
            }
        }

        private void UpdateWheelTrail(ParticleSystem smoke, TrailRenderer trail, bool isDrifting, bool isSkidding,
            bool wheelIsGrounded)
        {
            if (trail != null && wheelIsGrounded)
            {
                trail.emitting = isDrifting || isSkidding;
            }

            if (smoke != null && wheelIsGrounded)
            {
                var emission = smoke.emission;
                emission.enabled = isDrifting || isSkidding;
            }
        }
    }
}