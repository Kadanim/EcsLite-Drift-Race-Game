using Components.Player;
using Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems.Player
{
    public class PlayerCameraInitSystem : IEcsInitSystem
    {
        private readonly EcsPoolInject<PlayerCameraComponent> _cameraPool = default;
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<SceneData> _sceneData = default;

        public void Init(IEcsSystems systems)
        {
            var ecsWorld = _world.Value;
            int cameraEntity = ecsWorld.NewEntity();
            ref var cameraComponent = ref _cameraPool.Value.Add(cameraEntity);

            GameObject cameraObject = Camera.main.gameObject;
            cameraComponent.CameraTransform = cameraObject.transform;
            cameraComponent.PlayerTransform = _sceneData.Value.PlayerTransform;
            cameraComponent.Offset = _staticData.Value.CameraOffset;
            cameraComponent.CameraDistance = _staticData.Value.CameraDistance;
            cameraComponent.CameraSmooth = _staticData.Value.CameraSmooth;
            cameraComponent.PlayerRigidbody = cameraComponent.PlayerTransform.GetComponent<Rigidbody>();
        }
    }
}