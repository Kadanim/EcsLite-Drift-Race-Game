using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


public class GameManagerInitSystem : IEcsInitSystem
{
    private readonly EcsWorldInject _world = default;
    private readonly EcsPoolInject<GameManagerComponent> _gameManagerPool = default;

    public void Init(IEcsSystems systems)
    {
        var ecsWorld = _world.Value;
        int gameManagerEntity = ecsWorld.NewEntity();
        ref var gameManager = ref _gameManagerPool.Value.Add(gameManagerEntity);
        gameManager.TotalDriftScore = 0;
    }
}