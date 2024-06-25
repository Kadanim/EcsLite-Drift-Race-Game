using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
    public class DriftScoreSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerCarComponent, DriftComponent>> _filter = default;
        private readonly EcsFilterInject<Inc<GameManagerComponent>> _gameManagerFilter = default;
        private readonly EcsPoolInject<PlayerCarComponent> _carPool = default;
        private readonly EcsPoolInject<DriftComponent> _driftPool = default;
        private readonly EcsPoolInject<GameManagerComponent> _gameManagerPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var player = ref _carPool.Value.Get(i);
                ref var drift = ref _driftPool.Value.Get(i);

                float slip = Vector3.Dot(player.PlayerRigidbody.linearVelocity.normalized, player.PlayerTransform.forward);
                drift.IsDrifting = Mathf.Abs(slip) < 0.95f;

                if (drift.IsDrifting)
                {
                    float driftPoints = Time.deltaTime * player.PlayerRigidbody.linearVelocity.magnitude;
                    drift.DriftScore += driftPoints;
                    
                    foreach (var gm in _gameManagerFilter.Value)
                    {
                        ref var gameManager = ref _gameManagerPool.Value.Get(gm);
                        gameManager.TotalDriftScore += driftPoints;
                        Debug.Log(gameManager.TotalDriftScore);
                    }
                }
                else
                {
                    drift.DriftScore = 0;
                }
            }
        }
    }
}