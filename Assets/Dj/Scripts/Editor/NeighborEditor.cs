using System.Collections.Generic;
using System.Linq;
using CW.Scripts;
using UnityEditor;
using UnityEngine;

namespace Dj.Scripts.Editor
{
    [CustomEditor(typeof(Neighbor))]
    public class NeighborEditor : UnityEditor.Editor
    {
        protected Graph _graph;

        void OnEnable()
        {
            Neighbor neighbor = target as Neighbor;
            _graph = neighbor.GetComponent<Graph>();
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Build Paths"))
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

                        if (nodeCheck.connections.Contains(node) || nodeCheck.connections.Count > 2)
                        {
                            continue;
                        }

                        float dist = Vector2.Distance(node.transform.position, nodeCheck.transform.position);
                        Vector2 dir = (nodeCheck.transform.position - node.transform.position).normalized;
                        RaycastHit2D hit = Physics2D.Raycast(node.transform.position, dir, dist);
                        if (hit.collider != null)
                        {
                            continue;
                        }

                        node.connections.Add(nodeCheck);
                        added++;

                        if (added > 2)
                        {
                            break;
                        }
                    }
                }
                SceneView.RepaintAll();
            }

            if (GUILayout.Button("Clear Paths"))
            {
                List<Node> nodes = _graph.nodes;

                foreach (var node in nodes)
                {
                    node.connections.Clear();
                }
                
                SceneView.RepaintAll();
            }
            
            if (GUILayout.Button("Cat"))
            {
                Follower follower = GameObject.Find("Cat").GetComponent<Follower>();
                
                List<Node> nodes = _graph.nodes;
                List<Node> sorted = nodes
                    .OrderBy(point => Vector3.Distance(follower.transform.position, point.transform.position)).ToList();
                Node n1 = sorted.First();
                Node n2 = nodes[Random.Range(0, nodes.Count)];
			
                follower.Begin(n1, n2);
            }
        }
    }
}