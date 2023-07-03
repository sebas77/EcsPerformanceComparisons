using Svelto.ECS;
using Unity.Mathematics;

namespace Logic.SveltoECS
{
    public class VehicleMovementSystem : IQueryingEntitiesEngine, IStepEngine<float>
    {
        public void Step(in float deltaTime)
        {
            foreach (var ((vehicles, positions, count), _) in entitiesDB.QueryEntities<TargetDC, PositionDC>(VehicleGroup.Groups))
            {
                for (int i = 0; i < count; i++)
                {
                    var vehicle = vehicles[i].target;
                    if (vehicle.ToEGID(entitiesDB, out var egid)) //if false is either invalid or dead
                    {
                        ref var position = ref positions[i];
                        float2 currentPosition = position.Value;
                        //todo: querying entities inside a loop like this is a killer for cache.
                        float2 targetPosition = _mapped.Entity(egid).Value;

                        if (math.distance(currentPosition, targetPosition) < DefaultECS.Data.WeaponRange)
                            continue;

                        var direction = math.normalize(targetPosition - currentPosition);
                        var newPosition = currentPosition + direction * Data.VehicleSpeed * deltaTime;
                        position.Value = newPosition;
                    }
                }
            }
        }

        public void Ready()
        {
            _mapped = entitiesDB.QueryMappedEntities<PositionDC>(VehicleGroup.Groups);
        }

        public EntitiesDB entitiesDB { get; set; }
 
        public string name => nameof(VehicleMovementSystem);
        
        EGIDMultiMapperNB<PositionDC> _mapped;
    }
}