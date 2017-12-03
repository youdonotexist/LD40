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
        protected Neighbor _neighbor;

        void OnEnable()
        {
            _neighbor = target as Neighbor;
            _graph = _neighbor.GetComponent<Graph>();
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Build Paths"))
            {
                _neighbor.BuildPath();
                
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
        }
    }
}