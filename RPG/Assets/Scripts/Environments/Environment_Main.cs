using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace Tamana
{
    public class Environment_Main : MonoBehaviour
    {
        private static Dictionary<int, Transform> gameObjectDic;
        private static Dictionary<int, MeshCollider> meshColliderDic;

        public static readonly ConcurrentQueue<int> idWithDistanceBelow10 = new ConcurrentQueue<int>();
        public static readonly ConcurrentQueue<int> idWithDistanceBetween10and15 = new ConcurrentQueue<int>();

        private void Awake()
        {
            meshColliderDic = new Dictionary<int, MeshCollider>();
            gameObjectDic = new Dictionary<int, Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                GetAllChilds(transform.GetChild(i));
            }
        }

        private void Update()
        {
            var playerDistanceArray = new NativeArray<Environment_PlayerDistanceData>(gameObjectDic.Count, Allocator.TempJob);

            var playerPos = GameManager.PlayerTransform.position;
            var index = 0;
            foreach (var i in gameObjectDic)
            {
                playerDistanceArray[index] = new Environment_PlayerDistanceData(playerPos, i.Value.position, i.Key);
                index++;
            }

            var job = new Environment_PlayerDistanceJobHandler()
            {
                PlayerDistanceArray = playerDistanceArray
            };

            var jobHandle = job.Schedule(gameObjectDic.Count, 1);
            jobHandle.Complete();

            playerDistanceArray.Dispose();

            while (idWithDistanceBelow10.Count > 0)
            {
                var result = idWithDistanceBelow10.TryDequeue(out int id);

                if (result == true && meshColliderDic.ContainsKey(id) == false &&
                    gameObjectDic.ContainsKey(id) == true)
                {
                    var mc = gameObjectDic[id].gameObject.AddComponent<MeshCollider>();
                    mc.sharedMesh = gameObjectDic[id].gameObject.GetComponent<Environment_ObjectMeshHolder>().sharedMesh;
                    meshColliderDic.Add(id, mc);
                }
            }

            while (idWithDistanceBetween10and15.Count > 0)
            {
                var result = idWithDistanceBetween10and15.TryDequeue(out int id);

                if (result == true && meshColliderDic.ContainsKey(id) == true)
                {
                    Destroy(meshColliderDic[id]);
                    meshColliderDic.Remove(id);
                }
            }
        }

        private void GetAllChilds(Transform child)
        {
            for(int i = 0; i < child.childCount; i++)
            {
                var grandChild = child.GetChild(i);
                    
                var mr = grandChild.GetComponent<MeshRenderer>();
                if(mr != null)
                {
                    gameObjectDic.Add(grandChild.GetInstanceID(), grandChild);
                    if(mr.gameObject.GetComponent<Environment_ObjectMeshHolder>() == null)
                    {
                        mr.gameObject.AddComponent<Environment_ObjectMeshHolder>();
                    }                    
                }

                if(grandChild.childCount > 0)
                {
                    GetAllChilds(grandChild);
                }
            }
        }
    }
}
