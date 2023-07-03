using Svelto.ECS;

namespace Logic.SveltoECS
{
    public class EnemyTargetSystem: IQueryingEntitiesEngine, IStepEngine<float>
    {
        public void Ready() { }

        public EntitiesDB entitiesDB { get; set; }

        public void Step(in float deltaTime)
        {
            var teamsStillAlive = 0;

            foreach (var ((vehicles, teams, count), _) in entitiesDB.QueryEntities<TargetDC, TeamDC>(VehicleGroup.Groups))
            {
                for (var i = 0; i < count; ++i)
                {
                    ref var vehicle = ref vehicles[i];
                    if (vehicle.target.Exists(entitiesDB)) //the current vehicle has a target. Todo: this is not ECS oriented, we should have a sub-set of vehicles without target instead
                        continue;

                    //pick up a random team index to not always attack the same team
                    var enemyTeamIndex = UnityEngine.Random.Range(0, Data.MaxTeamCount - 1);
                    int iteration = 0;

                    while (iteration++ < Data.MaxTeamCount - 1) //search for target to attack in a team different than the entity one
                    {
                        enemyTeamIndex = (int)(teams[i].Value + enemyTeamIndex) % Data.MaxTeamCount;
                        //todo: jumping groups like this is a killer for the cache, it would be wiser to have a better strategy to pick up enemies to minimise the number of queries
                        var (_, entityIDs, enemyTeamCount) = entitiesDB.QueryEntities<TeamDC>(VehicleGroup.BuildGroup + (uint)enemyTeamIndex);
                        if (enemyTeamCount > 0)
                        { //get any random entity from a team with still alive vehicles
                            uint index = (uint)UnityEngine.Random.Range(0, enemyTeamCount);
                            var egid = new EGID(entityIDs[index], VehicleGroup.BuildGroup + (uint)enemyTeamIndex);
                            vehicle.target = entitiesDB.GetEntityReference(egid);

                            break;
                        }

                        enemyTeamIndex++;
                    }
                }
            }
        }

        public string name => nameof(EnemyTargetSystem);
    }
}