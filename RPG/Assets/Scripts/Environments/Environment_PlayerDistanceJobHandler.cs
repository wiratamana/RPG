using Unity.Jobs;
using Unity.Collections;

namespace Tamana
{
    public struct Environment_PlayerDistanceJobHandler : IJobParallelFor
    {
        public NativeArray<Environment_PlayerDistanceData> PlayerDistanceArray;

        public void Execute(int index)
        {
            var data = PlayerDistanceArray[index];
            data.Calculate();
            PlayerDistanceArray[index] = data;
        }
    }
}
