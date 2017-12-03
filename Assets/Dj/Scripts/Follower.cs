using System.Collections;
using System.Collections.Generic;
using CW.Scripts;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

/// <summary>
/// The Follower.
/// </summary>
[ExecuteInEditMode]
public class Follower : MonoBehaviour
{
    [SerializeField] protected Graph m_Graph;
    [SerializeField] protected Node m_Start;
    [SerializeField] protected Node m_End;
    [SerializeField] protected float m_Speed = 0.01f;
    private Rigidbody2D _rigidbody2D;
    protected Path m_Path = new Path();
    protected Node m_Current;
    protected IPathTracker _pathTracker;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Begin(Node start, Node end, IPathTracker tracker)
    {
        m_Graph = GameObject.Find("Nav").GetComponent<Graph>();
        m_Start = start;
        m_End = end;
        m_Path = m_Graph.GetShortestPath(m_Start, m_End);
        _pathTracker = tracker;
        Follow(m_Path);
    }

    /// <summary>
    /// Follow the specified path.
    /// </summary>
    /// <param name="path">Path.</param>
    public void Follow(Path path)
    {
        StopCoroutine("FollowPath");
        m_Path = path;
        transform.position = m_Path.nodes[0].transform.position;
        StartCoroutine("FollowPath");
    }

    /// <summary>
    /// Following the path.
    /// </summary>
    /// <returns>The path.</returns>
    IEnumerator FollowPath()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.update += Update;
#endif
        var e = m_Path.nodes.GetEnumerator();
        while (e.MoveNext())
        {
            m_Current = e.Current;

            if (m_Current)
            {
                _pathTracker.OnDirectionChange((m_Current.transform.position - transform.position).normalized);
            }

            Debug.Log("Inside node processing");
            
            // Wait until we reach the current target node and then go to next node
            yield return new WaitUntil(delegate
            {
                Debug.Log("waiting for event..");
                return Vector2.Distance(transform.position, m_Current.transform.position) <
                       (m_Speed * Time.deltaTime) || m_Path == null;
            });
        }
        Node tmp = m_Current;
        m_Current = null;
        _pathTracker.OnCompletePath(tmp);
        e.Dispose();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.update -= Update;
#endif
    }

    void Update()
    {
        if (m_Current != null && m_Path != null)
        {
            Vector2 walk = (m_Current.transform.position - transform.position).normalized;
            _rigidbody2D.MovePosition(_rigidbody2D.position + (walk * m_Speed * Time.deltaTime));
        }
    }

    public void Stop()
    {
        StopCoroutine("FollowPath");
        m_Path = null;
        m_Start = null;
        m_End = null;
    }
}