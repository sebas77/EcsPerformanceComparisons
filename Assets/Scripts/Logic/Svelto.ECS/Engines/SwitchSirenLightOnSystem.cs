// Copyright (c) Sean Nowotny

using Svelto.ECS;

namespace Logic.SveltoECS
{
    public class SwitchSirenLightOnSystem : IQueryingEntitiesEngine, IStepEngine<float>
    {
//        public SwitchSirenLightOnSystem(World _world) : base(
//            _world.GetEntities().With<TargetDC>().With<HealthDC>().With<TimeUntilSirenSwitch>().Without<SirenLight>().AsSet(),
//            false
//        )
//        {
//        }
//
//        protected override void Update(float _deltaTime, in Entity entity)
//        {
//            var health = entity.Get<HealthDC>().Value;
//            var timeUntilSirenSwitch = entity.Get<TimeUntilSirenSwitch>().Value;
//
//            if (timeUntilSirenSwitch <= 0)
//            {
//                entity.Set(
//                    new SirenLight
//                    {
//                        LightIntensity = (int)math.min(150 - health, 100)
//                    }
//                );
//                entity.Set(
//                    new TimeUntilSirenSwitch
//                    {
//                        Value = health / 100
//                    }
//                );
//            }
//        }

        public void Ready()
        {
        }

        public EntitiesDB entitiesDB { get; set; }
        public void Step(in float _param)
        {
        }

        public string name => nameof(SwitchSirenLightOnSystem);
    }
}