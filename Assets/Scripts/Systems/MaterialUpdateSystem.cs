using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
    public class MaterialUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerCarComponent>> _filter = default;
        private readonly EcsPoolInject<PlayerInputData> _inputPool = default;
        private readonly EcsPoolInject<MaterialComponent> _materialPool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var input = ref _inputPool.Value.Get(i);
                ref var material = ref _materialPool.Value.Get(i);
                
                if (input.GasInput < 0)
                {
                    SetEmission(true, material.BackLightMaterials);
                }
                else
                {
                    SetEmission(false, material.BackLightMaterials);
                }
            }
        }

        private void SetEmission(bool enabled, Material[] materials)
        {
            foreach (var mat in materials)
            {
                if (enabled)
                {
                    mat.SetFloat("_EmissiveExposureWeight", 0);
                }
                else
                {
                    mat.SetFloat("_EmissiveExposureWeight", 1);
                }
            }
        }
    }
}