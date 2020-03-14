using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class PF_Master : SingletonMonobehaviour<PF_Master>
    {
        [SerializeField] private List<PF_Node> nodes = new List<PF_Node>();

        private void OnValidate()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                var node = transform.GetChild(i).GetComponent<PF_Node>();

                if(node == null)
                {
                    node = transform.GetChild(i).gameObject.AddComponent<PF_Node>();
                }

                if(nodes.Contains(node))
                {
                    continue;
                }

                nodes.Add(node);
            }
        }

        public PF_Node GetNearestNode(Vector3 position)
        {
            PF_Node retval = null;
            float distance = float.MaxValue;

            foreach (var n in nodes)
            {
                var dis = Vector3.Distance(position, n.transform.position);
                if(dis < distance)
                {
                    distance = dis;
                    retval = n;
                }
            }

            return retval;
        }
    }
}
