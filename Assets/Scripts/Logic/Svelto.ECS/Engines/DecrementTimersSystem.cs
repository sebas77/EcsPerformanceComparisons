// Copyright (c) Sean Nowotny

using Svelto.ECS;
using Unity.Mathematics;

namespace Logic.SveltoECS
{
    public class DecrementTimersSystem: IQueryingEntitiesEngine, IStepEngine<float>
    {
//        public DecrementTimersSystem(World _world) : base(
//            _world.GetEntities().With<TimeUntilSirenSwitch>().AsSet(),
//            false
//        )
//        {
//        }
//
//        protected override void Update(float _deltaTime, in Entity entity)
//        {
//            entity.Set(new TimeUntilSirenSwitch
//                {
//                    Value = entity.Get<TimeUntilSirenSwitch>().Value - _deltaTime
//                }
//            );
//        }
        public void Ready() { }

        public EntitiesDB entitiesDB { get; set; }
        public void Step(in float _param) { }

        public string name => nameof(DecrementTimersSystem);
    }
}