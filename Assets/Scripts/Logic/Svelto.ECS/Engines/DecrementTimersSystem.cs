// Copyright (c) Sean Nowotny

using Svelto.ECS;

namespace Logic.SveltoECS
{
    public class DecrementTimersSystem: IQueryingEntitiesEngine, IStepEngine<float>
    {
        public void Ready() { }

        public EntitiesDB entitiesDB { get; set; }

        public void Step(in float time)
        {
            entitiesDB.QueryUniqueEntity<TimeUntilSirenSwitch>(ExclusiveGroups.TimeGroup).Value -= time;
        }

        public string name => nameof(DecrementTimersSystem);
    }
}