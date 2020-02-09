using UnityEngine;
using System.Diagnostics;

namespace Tamana
{
    public static class Benchmarker
    {
        private static readonly Stopwatch stopwatch = new Stopwatch();

        public static void Start(string startMessage = null)
        {
            stopwatch.Start();
            if(string.IsNullOrEmpty(startMessage) == false)
            {
                Debug.Log($"startMessage : {startMessage}", Debug.LogType.Message);
            }
        }

        public static void Stop(string stopMessage = null)
        {
            stopwatch.Stop();
            Debug.Log($"Elapsed times : {stopwatch.ElapsedMilliseconds}ms | Elapsed ticks : {stopwatch.ElapsedTicks}");
            if(string.IsNullOrEmpty(stopMessage) == false)
            {
                Debug.Log($"stopMessage : {stopMessage}", Debug.LogType.Message);
            }
            stopwatch.Reset();
        }
    }
}
