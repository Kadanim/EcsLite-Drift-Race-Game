using Components.GameManager;
using Components.UI;
using Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems.UI
{
    public class UIInitSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<DriftScoreUIComponent> _uiPool = default;
        private readonly EcsPoolInject<GameManagerComponent> _gameManagerPool = default;
        private readonly EcsCustomInject<SceneData> _sceneData = default;

        public void Init(IEcsSystems systems)
        {
            var world = _world.Value;
            int entity = world.NewEntity();

            ref var uiComponent = ref _uiPool.Value.Add(entity);
            uiComponent.DriftScoreText = _sceneData.Value.DriftScoreText;

            ref var gameManagerComponent = ref _gameManagerPool.Value.Add(entity);
            gameManagerComponent.TotalDriftScore = 0;
        }
    }
}