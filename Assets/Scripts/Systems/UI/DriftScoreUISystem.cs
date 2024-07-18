using Components.GameManager;
using Components.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems.UI
{
    public class DriftScoreUISystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DriftScoreUIComponent, GameManagerComponent>> _filter = default;
        private readonly EcsPoolInject<DriftScoreUIComponent> _uiPool = default;
        private readonly EcsPoolInject<GameManagerComponent> _gameManagerPool = default;
        private int _lastDriftScore = -1;


        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var uiComponent = ref _uiPool.Value.Get(i);
                ref var gameManagerComponent = ref _gameManagerPool.Value.Get(i);
                int totalDriftScore = Mathf.FloorToInt(gameManagerComponent.TotalDriftScore);

                if (totalDriftScore != _lastDriftScore)
                {
                    uiComponent.DriftScoreText.text = $"Drift Score: {totalDriftScore}";
                    _lastDriftScore = totalDriftScore;
                }
            }
        }
    }
}