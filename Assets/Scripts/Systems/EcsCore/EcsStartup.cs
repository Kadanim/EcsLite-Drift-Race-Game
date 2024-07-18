using Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Systems.Material;
using Systems.Player;
using Systems.UI;
using Systems.Wheel;
using UnityEngine;

namespace Systems.EcsCore
{
    public class EcsStartup : MonoBehaviour
    {
        public StaticData Configuration;
        public SceneData SceneData;
        private EcsWorld _world;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;

        void Start()
        {
            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            RuntimeData runtimeData = new RuntimeData();

            _updateSystems
                .Add(new PlayerInitSystem())
                .Add(new PlayerCameraInitSystem())
                .Add(new UIInitSystem())
                .Add(new GroundCheckSystem())
                .Add(new PlayerInputSystem())
                .Add(new MaterialUpdateSystem())
                .Add(new WheelEffectsUpdateSystem())
                .Add(new DriftScoreSystem())
                .Add(new DriftScoreUISystem())
                .Inject(_world)
                .Inject(Configuration)
                .Inject(SceneData)
                .Inject(runtimeData)
                .Init();


            _fixedUpdateSystems
                .Add(new PlayerCarMoveSystem())
                .Add(new PlayerCameraUpdateSystem())
                .Inject(_world)
                .Inject(Configuration)
                .Inject(SceneData)
                .Inject(runtimeData)
                .Init();
        }

        void Update()
        {
            _updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        void OnDestroy()
        {
            _updateSystems?.Destroy();
            _updateSystems = null;
            _fixedUpdateSystems?.Destroy();
            _fixedUpdateSystems = null;
            _world?.Destroy();
            _world = null;
        }
    }
}