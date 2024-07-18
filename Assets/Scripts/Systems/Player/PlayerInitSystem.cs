using Components.Drift;
using Components.Material;
using Components.Player;
using Components.Wheel;
using Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems.Player
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsPoolInject<PlayerCarComponent> _carPool = default;
        private readonly EcsPoolInject<WheelComponent> _wheelPool = default;
        private readonly EcsPoolInject<WheelSmokeComponent> _smokePool = default;
        private readonly EcsPoolInject<WheelTrailComponent> _trailPool = default;
        private readonly EcsPoolInject<MaterialComponent> _materialPool = default;
        private readonly EcsPoolInject<PlayerInputData> _inputPool = default;
        private readonly EcsPoolInject<DriftComponent> _driftPool = default;
        private readonly EcsPoolInject<GroundCheckComponent> _groundCheckPool = default;
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<SceneData> _sceneData = default;

        public void Init(IEcsSystems systems)
        {
            var ecsWorld = _world.Value;
            int playerEntity = ecsWorld.NewEntity();

            ref var player = ref _carPool.Value.Add(playerEntity);
            ref var wheels = ref _wheelPool.Value.Add(playerEntity);
            ref var smoke = ref _smokePool.Value.Add(playerEntity);
            ref var trail = ref _trailPool.Value.Add(playerEntity);
            ref var material = ref _materialPool.Value.Add(playerEntity);
            ref var groundCheck = ref _groundCheckPool.Value.Add(playerEntity);
            _inputPool.Value.Add(playerEntity);
            _driftPool.Value.Add(playerEntity);

            GameObject playerCar = Object.Instantiate(_staticData.Value.PlayerPrefab,
                _sceneData.Value.PlayerSpawnPoint.position, Quaternion.identity);

            InitializePlayer(ref player, playerCar);
            InitializeWheels(ref wheels, playerCar);
            InitializeSmoke(ref smoke, ref wheels);
            InitializeTrail(ref trail, ref wheels);
            InitializeMaterials(ref material);

            player.MotorPower = _staticData.Value.MotorPower;
            player.BrakePower = _staticData.Value.BrakePower;
            player.SteeringCurve = _staticData.Value.SteeringCurve;

            _sceneData.Value.PlayerTransform = player.PlayerTransform;
        }

        private void InitializePlayer(ref PlayerCarComponent player, GameObject playerCar)
        {
            player.PlayerRigidbody = playerCar.GetComponent<Rigidbody>();
            player.PlayerTransform = playerCar.GetComponent<Transform>();
        }

        private void InitializeWheels(ref WheelComponent wheels, GameObject playerCar)
        {
            Transform wheelColliders = playerCar.transform.GetChild(0);
            wheels.FrWheelCollider = wheelColliders.GetChild(0).GetComponent<WheelCollider>();
            wheels.FlWheelCollider = wheelColliders.GetChild(1).GetComponent<WheelCollider>();
            wheels.RrWheelCollider = wheelColliders.GetChild(2).GetComponent<WheelCollider>();
            wheels.RlWheelCollider = wheelColliders.GetChild(3).GetComponent<WheelCollider>();

            Transform wheelMeshes = playerCar.transform.GetChild(1);
            wheels.FrWheelMesh = wheelMeshes.GetChild(0).GetComponent<MeshRenderer>();
            wheels.FlWheelMesh = wheelMeshes.GetChild(1).GetComponent<MeshRenderer>();
            wheels.RrWheelMesh = wheelMeshes.GetChild(2).GetComponent<MeshRenderer>();
            wheels.RlWheelMesh = wheelMeshes.GetChild(3).GetComponent<MeshRenderer>();
        }

        private void InitializeSmoke(ref WheelSmokeComponent smoke, ref WheelComponent wheels)
        {
            smoke.FrWheelSmoke = wheels.FrWheelCollider.GetComponentInChildren<ParticleSystem>();
            smoke.FlWheelSmoke = wheels.FlWheelCollider.GetComponentInChildren<ParticleSystem>();
            smoke.RrWheelSmoke = wheels.RrWheelCollider.GetComponentInChildren<ParticleSystem>();
            smoke.RlWheelSmoke = wheels.RlWheelCollider.GetComponentInChildren<ParticleSystem>();
        }

        private void InitializeTrail(ref WheelTrailComponent trail, ref WheelComponent wheels)
        {
            trail.FrWheelTrail = wheels.FrWheelCollider.GetComponentInChildren<TrailRenderer>();
            trail.FlWheelTrail = wheels.FlWheelCollider.GetComponentInChildren<TrailRenderer>();
            trail.RrWheelTrail = wheels.RrWheelCollider.GetComponentInChildren<TrailRenderer>();
            trail.RlWheelTrail = wheels.RlWheelCollider.GetComponentInChildren<TrailRenderer>();
        }

        private void InitializeMaterials(ref MaterialComponent material)
        {
            material.BackLightMaterials = new UnityEngine.Material[4];
            Renderer renderer = GameObject.FindGameObjectWithTag("BackLight").GetComponent<Renderer>();
            material.BackLightMaterials[0] = renderer.materials[0];
            material.BackLightMaterials[1] = renderer.materials[1];
            material.BackLightMaterials[2] = renderer.materials[2];
            material.BackLightMaterials[3] = renderer.materials[3];
        }
    }
}