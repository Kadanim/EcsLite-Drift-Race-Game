using Components.Player;
using Components.Wheel;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems.Wheel
{
    public class GroundCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerCarComponent, GroundCheckComponent>> _filter = default;
        private readonly EcsPoolInject<GroundCheckComponent> _groundCheckPool = default;
        private readonly EcsPoolInject<WheelComponent> _wheelsPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var groundCheck = ref _groundCheckPool.Value.Get(i);
                ref var wheels = ref _wheelsPool.Value.Get(i);

                groundCheck.FrWheelIsGrounded = wheels.FrWheelCollider.isGrounded;
                groundCheck.FlWheelIsGrounded = wheels.FlWheelCollider.isGrounded;
                groundCheck.RrWheelIsGrounded = wheels.RrWheelCollider.isGrounded;
                groundCheck.RlWheelIsGrounded = wheels.RlWheelCollider.isGrounded;

                groundCheck.CarCompletelyIsGrounded =
                    wheels.FrWheelCollider.isGrounded && wheels.FlWheelCollider.isGrounded &&
                    wheels.RrWheelCollider.isGrounded && wheels.RlWheelCollider.isGrounded;

                groundCheck.CarPartlyIsGrounded =
                    wheels.FrWheelCollider.isGrounded || wheels.FlWheelCollider.isGrounded ||
                    wheels.RrWheelCollider.isGrounded || wheels.RlWheelCollider.isGrounded;
            }
        }
    }
}