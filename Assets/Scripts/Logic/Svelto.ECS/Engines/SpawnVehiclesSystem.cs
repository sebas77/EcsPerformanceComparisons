using Svelto.ECS;
using Svelto.ECS.Experimental;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace Logic.SveltoECS
{
//    public static class VechilesFilterIds
//    {
//        internal static readonly FilterContextID VehicleFilterContext = FilterContextID.GetNewContextID();
//    }
//
    public class SpawnVehiclesSystem: IQueryingEntitiesEngine, IStepEngine<float>
    {
        public SpawnVehiclesSystem(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public void Step(in float deltaTime)
        {
            QueryGroups query = new QueryGroups(VehicleGroup.Groups);
            var aliveCount = query.Evaluate().Count<TeamDC>(entitiesDB);

            var maxTeamCount = Data.MaxTeamCount;
            var maxVehicleCount = Data.MaxVehicleCount;

            for (uint i = 0; i < maxTeamCount; i++)
            {
                if (aliveCount == maxVehicleCount)
                {
                    break;
                }

                var egid = new EGID((uint)Count++, VehicleGroup.BuildGroup + i);
                var init = _entityFactory.BuildEntity<VehicleDescriptor>(egid);

                init.Init(
                    new TeamDC()
                    {
                        Value = i
                    });
                init.Init(
                    new PositionDC
                    {
                        Value = new float2(Random.Range(0, 100), Random.Range(0, 100))
                    });
                init.Init(
                    new HealthDC
                    {
                        Value = 100
                    });

                aliveCount++;
            }
        }

        public void Ready()
        { }

        public EntitiesDB entitiesDB { get; set; }

        public string name => nameof(SpawnVehiclesSystem);

        readonly IEntityFactory _entityFactory;
        
        static int Count = 0;
    }
};