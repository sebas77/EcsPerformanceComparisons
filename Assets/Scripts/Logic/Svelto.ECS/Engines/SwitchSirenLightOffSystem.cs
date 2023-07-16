// Copyright (c) Sean Nowotny

using Svelto.ECS;

namespace Logic.SveltoECS
{
    public class SwitchSirenLightOffSystem: IQueryingEntitiesEngine, IStepEngine<float>
    {
        public SwitchSirenLightOffSystem(IEntityFunctions entityFunctions)
        {
            _entityFunctions = entityFunctions;
        }

        //        protected override void Update(float _deltaTime, in Entity entity)
//        {
//            var timeUntilSirenSwitch = entity.Get<TimeUntilSirenSwitch>().Value;
//
//            if (timeUntilSirenSwitch <= 0)
//            {
//                entity.Remove<SirenLight>();
//                entity.Set(
//                    new TimeUntilSirenSwitch
//                    {
//                        Value = 0.1f
//                    }
//                );
//            }
//        }
        public void Ready() { }

        public EntitiesDB entitiesDB { get; set; }
        public void Step(in float time) { }

        public string name => nameof(SwitchSirenLightOffSystem);
        
        readonly IEntityFunctions _entityFunctions;
    }
}