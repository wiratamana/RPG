using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [ExecuteAlways]
    public class PF_Node : MonoBehaviour
    {
        public List<PF_Node> neighbours = new List<PF_Node>();
        [SerializeField] private bool enableAutoUpdate = true;

        private void OnValidate()
        {
            name = transform.position.ToString();
            if(neighbours.Count == 0)
            {
                name = "Empty Node";
            }

            for (int i  = 0; i < neighbours.Count; i++)
            {
                if (neighbours[i].neighbours.Contains(this) == false)
                {
                    if (enableAutoUpdate == true)
                    {
                        neighbours[i].neighbours.Add(this);
                    }
                }

                if (neighbours[i] == null)
                {
                    neighbours.RemoveAt(i);
                    i--;
                    continue;
                }

            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.5f);

            foreach (var n in neighbours)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(n.transform.position, 0.5f);

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, n.transform.position);
            }
        }
    }
}
