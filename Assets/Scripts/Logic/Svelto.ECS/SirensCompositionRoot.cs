using System.Threading.Tasks;
using Svelto.Context;
using Svelto.ECS;
using Svelto.ECS.Schedulers;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Logic.SveltoECS
{
    public class SirensSequentialEngines: UnsortedEnginesGroup<IStepEngine<float>, float>
    { }
    
    public class SirensCompositionRoot: ICompositionRoot
    {
        SimpleEntitiesSubmissionScheduler _ticker;
        SirensSequentialEngines _sequentialEnginesGroup;

        public void OnContextInitialized<T>(T contextHolder)
        {
        }

        public void OnContextDestroyed(bool hasBeenInitialised)
        {
        }

        public void OnContextCreated<T>(T contextHolder)
        {
            _ticker = new SimpleEntitiesSubmissionScheduler();
            var enginesRoot = new EnginesRoot(_ticker);
            var entityFunctions = enginesRoot.GenerateEntityFunctions();
            var entityFactory = enginesRoot.GenerateEntityFactory();
            
            _sequentialEnginesGroup = new SirensSequentialEngines();
            
            _sequentialEnginesGroup.Add(new SpawnVehiclesSystem(entityFactory));
            _sequentialEnginesGroup.Add(new EnemyTargetSystem());
            _sequentialEnginesGroup.Add(new VehicleMovementSystem());
    //        new SwitchSirenLightOnSystem(world),
  //          new SwitchSirenLightOffSystem(world),
//            new DecrementTimersSystem(world),
//                new ShootSystem(world),
//                new DieSystem(world)
            
            enginesRoot.AddEngine(_sequentialEnginesGroup);

            MainLoop();
        }

        public async void MainLoop()
        {
            while (Application.isPlaying)
            {
                _ticker.SubmitEntities();
                
                _sequentialEnginesGroup.Step(Time.deltaTime);

                if (Input.GetKeyUp(KeyCode.Space))
                {
//                    Data.EnableRendering = !Data.EnableRendering;
//                    if (Data.EnableRendering)
//                    {
//                        renderSystem.Initialize();
//                    }
//                    else
//                    {
//                        renderSystem.Clear();
//                    }
                }

//                if (Data.EnableRendering)
//                {
//                    renderSystem.Update(Time.deltaTime);
//                }
        
                await Task.Yield();
            }
        }
    }
}