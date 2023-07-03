using System;
using Svelto.ECS;
using UnityEngine;

namespace Logic.SveltoECS
{
    public class RenderSystem: IQueryingEntitiesEngine, IStepEngine<float>
    {
        private static Transform[] transformPool = new Transform[Data.MaxVehicleCount];
        private static MeshRenderer[] meshPool = new MeshRenderer[Data.MaxVehicleCount];

        public RenderSystem(GameObject prefab, Material[] materials, Material sirenLightMaterial)
        {
            _prefab = prefab;
            _materials = materials;
            _sirenLightMaterial = sirenLightMaterial;

         //   colorfulEntitiesSet = world.GetEntities().With<PositionDC>().Without<SirenLight>().AsSet();
          //  sirenLightEntitiesSet = world.GetEntities().With<PositionDC>().With<SirenLight>().AsSet();

            Initialize();
        }
        
        public void Initialize()
        {
            for (var i = 0; i < transformPool.Length; i++)
            {
                transformPool[i] = GameObject.Instantiate(_prefab).transform;
                meshPool[i] = transformPool[i].GetComponent<MeshRenderer>();
            }
        }

        public void Clear()
        {
            for (var i = 0; i < transformPool.Length; i++)
            {
                GameObject.Destroy(transformPool[i].gameObject);
            }
        }

        public void Step(in float _param)
        {
//            var colorfulEntities = colorfulEntitiesSet.GetEntities();
//            var colorfulEntitiesLength = colorfulEntities.Length;
//
//            var sirenLightEntities = sirenLightEntitiesSet.GetEntities();
//
//            UpdateVehicleColors(colorfulEntities, false, 0);
//            UpdateVehicleColors(sirenLightEntities, true, colorfulEntitiesLength);
//
//            var entitiesCount = colorfulEntities.Length + sirenLightEntities.Length;
//            for (var i = entitiesCount; i < Data.MaxVehicleCount; i++)
//            {
//                meshPool[i].enabled = false;
//            }
        }

//        private void UpdateVehicleColors(ReadOnlySpan<Entity> entities, bool sirenLightLit, int poolStartIndex)
//        {
//            for (var i = 0; i < entities.Length; i++)
//            {
//                int poolIndex = poolStartIndex + i;
//
//                var position = entities[i].Get<PositionDC>().Value;
//                transformPool[poolIndex].position = new Vector3(position.x, 0, position.y);
//                meshPool[poolIndex].enabled = true;
//
//                if (sirenLightLit)
//                {
//                    // TODO: Light intensity
//                    meshPool[poolIndex].material = sirenLightMaterial;
//                }
//                else
//                {
//                    meshPool[poolIndex].material = materials[entities[i].Get<TeamDC>().Value];
//                }
//            }
//        }

        public void Dispose()
        {}

        public void Ready()
        { }

        public EntitiesDB entitiesDB { get; set; }

        public string name => nameof(RenderSystem);
        
        private readonly GameObject _prefab;
        private readonly Material[] _materials;
        private readonly Material _sirenLightMaterial;
    }
}