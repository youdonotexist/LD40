using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CW.Scripts
{
    [ExecuteInEditMode]
    public class Neighbor : MonoBehaviour
    {
        [SerializeField] private Graph _graph;
        [SerializeField] private LayerMask _layerMask;

        private bool recalc = false;

        private static Neighbor _this;

        public static Neighbor Instance()
        {
            if (_this == null)
            {
                _this = GameObject.Find("Nav").GetComponent<Neighbor>();
            }

            return _this;
        }

        public void RedoPath()
        {
            recalc = true;
        }
        
        void Update()
        {
            if (recalc)
            {
                BuildPath();
                recalc = false;
            }
            
        }

        public void BuildPath()
        {
            List<Node> nodes = _graph.nodes;

            foreach (var node in nodes)
            {
                node.connections.Clear();
            }

            foreach (var node in nodes)
            {
                List<Node> sorted = nodes
                    .OrderBy(point => Vector3.Distance(node.transform.position, point.transform.position)).ToList();

                int added = 0;
                foreach (var nodeCheck in sorted)
                {
                    if (nodeCheck == node)
                    {
                        continue;
                    }

                    float dist = Vector2.Distance(node.transform.position, nodeCheck.transform.position);
                    Vector2 dir = (nodeCheck.transform.position - node.transform.position).normalized;
                    RaycastHit2D hit = Physics2D.Raycast(node.transform.position, dir, dist, _layerMask);
                    if (hit.collider != null)
                    {
                        continue;
                    }

                    node.connections.Add(nodeCheck);
                    added++;

                    if (added > 4)
                    {
                        break;
                    }
                }
            }
        }
    }
}