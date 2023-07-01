using Svelto.ECS;

namespace Logic.SveltoECS
{
    public class VehicleGroup:GroupTag<VehicleGroup>
    {
        static VehicleGroup()
        {
            range = (ushort)Data.MaxTeamCount;
        }
    }
}