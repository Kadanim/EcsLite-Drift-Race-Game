using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
    public class EcsStartup : MonoBehaviour
    {
        public StaticData _configuration;
        public SceneData _sceneData;
        private EcsWorld _world;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;
    
        void Start()
        {
            _world = new EcsWorld();
            _updateSystems  = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            RuntimeData runtimeData = new RuntimeData();

            _updateSystems
                .Add(new PlayerInitSystem())
                .Add(new PlayerCameraInitSystem())
                .Add(new GameManagerInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new MaterialUpdateSystem())
                .Inject(_world)
                .Inject(_configuration)
                .Inject(_sceneData)
                .Inject(runtimeData)
                .Init();
                
            
        
            _fixedUpdateSystems
                .Add(new PlayerCarMoveSystem())
                .Add(new PlayerCameraUpdateSystem())
                .Add(new WheelEffectsUpdateSystem())
                .Add(new DriftScoreSystem())
                .Inject(_world)  
                .Inject(_configuration)
                .Inject(_sceneData)
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