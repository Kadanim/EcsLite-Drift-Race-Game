using Components.Player;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems.Player
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerInputData>> _filter = default;
        private readonly EcsPoolInject<PlayerInputData> _inputPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var input = ref _inputPool.Value.Get(i);

                input.GasInput = Input.GetAxis("Vertical");
                input.SteeringInput = Input.GetAxis("Horizontal");
                input.HandBrakeInput = Input.GetKey(KeyCode.Space);
            }
        }
    }
}