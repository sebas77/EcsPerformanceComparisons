using Svelto.ECS;
using Unity.Mathematics;

namespace Logic.SveltoECS
{
    public class VehicleMovementSystem : IQueryingEntitiesEngine, IStepEngine<float>
    {
        public void Step(in float deltaTime)
        {
            foreach (var ((entities, positions, aliveCount), _) in entitiesDB.QueryEntities<TargetDC, PositionDC>(VehicleGroup.Groups))
            {
                for (int i = aliveCount - 1; i >= 0; i--)
                {
                    var targetEntity = entities[i].target;
                    if (targetEntity == EntityReference.Invalid)
                        continue;

                    ref var position = ref positions[i];
                    float2 currentPosition = position.Value;
                    //todo: querying entities inside a loop like this is a killer for cache.
                    float2 targetPosition = entitiesDB.QueryEntity<PositionDC>(targetEntity.ToEGID(entitiesDB)).Value;

                    if (math.distance(currentPosition, targetPosition) < DefaultECS.Data.WeaponRange)
                        continue;

                    var direction = math.normalize(targetPosition - currentPosition);
                    var newPosition = currentPosition + direction * Data.VehicleSpeed  * deltaTime;
                    position.Value = newPosition;
                }
            }
        }

        public void Ready() { }

        public EntitiesDB entitiesDB { get; set; }
 
        public string name => nameof(VehicleMovementSystem);
    }
}