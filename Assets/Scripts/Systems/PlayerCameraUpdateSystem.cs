using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
    public class PlayerCameraUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerCameraComponent>> _filter = default;
        private readonly EcsPoolInject<PlayerCameraComponent> _cameraPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var cameraComponent = ref _cameraPool.Value.Get(i);

                Vector3 playerForward = (cameraComponent.PlayerRigidbody.linearVelocity + cameraComponent.PlayerTransform.forward).normalized;

                cameraComponent.CameraTransform.position = Vector3.Lerp(cameraComponent.CameraTransform.position,
                    cameraComponent.PlayerTransform.position 
                    + cameraComponent.PlayerTransform.TransformVector(cameraComponent.Offset) 
                    + playerForward * cameraComponent.CameraDistance,
                    cameraComponent.CameraSmooth * Time.deltaTime);

                cameraComponent.CameraTransform.LookAt(cameraComponent.PlayerTransform);
            }
        }
    }
}