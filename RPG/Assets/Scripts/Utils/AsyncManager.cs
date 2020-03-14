using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AsyncManager : MonoBehaviour
    {
        private static int frameCount;

        private static List<AsyncAwaiter> frameAwaiterList;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Initialize()
        {
            var A = new GameObject(nameof(AsyncManager)).AddComponent<AsyncManager>();
            DontDestroyOnLoad(A.gameObject);

            frameAwaiterList = new List<AsyncAwaiter>();
        }

        private void Update()
        {
            frameCount++;

            for(int i = 0; i < frameAwaiterList.Count; i++)
            {
                frameAwaiterList[i].CurrentFrame = frameCount;

                if(frameAwaiterList[i].IsCompleted == true)
                {
                    frameAwaiterList.RemoveAt(i);
                    i--;
                }
            }
        }

        public static AsyncAwaiter WaitForFrame(int frames)
        {
            frameAwaiterList.Add(new AsyncAwaiter(frameCount + frames));
            return frameAwaiterList[frameAwaiterList.Count - 1];
        }
    }
}
