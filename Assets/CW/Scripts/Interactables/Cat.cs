using System.Collections.Generic;
using CW.Scripts.UI;
using UnityEngine;

namespace CW.Scripts.Interactables
{
    public class Cat : Interactable, IPathTracker
    {
        [SerializeField] private AudioClip _meowClip;
        [SerializeField] private Sprite _idleUp;
        [SerializeField] private Sprite _idleDown;
        [SerializeField] private Sprite _idleLeft;
        [SerializeField] private Sprite _idleRight;

        private AudioSource _audioSource;
        private Collider2D _collider2D;
        private Rigidbody2D _rigidbody2D;
        private PlayerAnimation _playerAnimation;
        private Follower _follower;
        private SpriteRenderer _spriteRenderer;
        private Node _walkToNode;

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _collider2D = GetComponent<Collider2D>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _follower = GetComponent<Follower>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Use this for initialization
        void Start()
        {
            FindStartNode();
        }

        private void Update()
        {
            if (_walkToNode != null)
            {
                if (Vector2.Distance(transform.position, _walkToNode.transform.position) <
                    (_follower.m_Speed * Time.deltaTime))
                {

                    if (_walkToNode.connections.Count > 0)
                    {
                        FindPath(_walkToNode);
                        _walkToNode = null;
                        _playerAnimation.PauseAnimation = false;
                    }
                    else
                    {
                        _playerAnimation.PauseAnimation = true;
                        _spriteRenderer.sprite = IdleForDirection(Vector2.right);
                    }
                }
                else
                {
                    Debug.Log("Walking to node..");

                    Vector2 walk = (_walkToNode.transform.position - transform.position).normalized;
                    _playerAnimation.Walk(walk);
                    _rigidbody2D.MovePosition(_rigidbody2D.position + (walk * _follower.m_Speed * Time.deltaTime));
                }
            }
            else
            {
                Debug.Log("Nowhere to go..");
            }
        }

        private void FindStartNode()
        {
            _walkToNode = _follower.FindClosest();
        }

        public override void Interact(Player player, Interactions interaction)
        {
            if (interaction == Interactions.Pet)
            {
                _audioSource.PlayOneShot(_meowClip);
            }
            else if (interaction == Interactions.Pickup)
            {
                _collider2D.enabled = false;
                _follower.Stop();
                _playerAnimation.PauseAnimation = true;
                _playerAnimation.Walk(player.LastWalk);
                _walkToNode = null;
            }
            else if (interaction == Interactions.Drop)
            {
                _collider2D.enabled = true;
                _collider2D.isTrigger = true;
                _spriteRenderer.sprite = IdleForDirection(player.LastWalk);
                Invoke("DelayedFindPath", 3.0f);
            }
        }

        private Sprite IdleForDirection(Vector2 dir)
        {
            if (dir.y > 0.0f)
            {
                return _idleUp;
            }
            else if (dir.y < 0.0f)
            {
                return _idleDown;
            }
            else if (dir.x > 0.0f)
            {
                return _idleRight;
            }
            else
            {
                return _idleLeft;
            }
        }

        public override Dictionary<KeyCode, Interactions> InteractOptions(Player player)
        {
            Dictionary<KeyCode, Interactions> interactions = new Dictionary<KeyCode, Interactions>();
            if (player.HasPickedUpInteractable())
            {
                interactions.Add(KeyCode.Alpha1, Interactions.Drop);
            }
            else
            {
                interactions = new Dictionary<KeyCode, Interactions>()
                {
                    {KeyCode.Alpha1, Interactions.Pickup},
                    {KeyCode.Alpha2, Interactions.Pet},
                    {KeyCode.Alpha3, Interactions.Kill}
                };
            }

            return interactions;
        }

        public override bool IsAvailable()
        {
            return true;
        }

        public override void SetDirection(Vector2 walk)
        {
            if (transform.parent == null)
            {
                _playerAnimation.Walk(walk);
            }
            else
            {
                _spriteRenderer.sprite = IdleForDirection(walk);
            }
        }

        public void OnCompletePath(Node lastNode)
        {
            Debug.Log("Completed Path For: " + gameObject.name);
            FindPath(lastNode);
        }

        public void OnDirectionChange(Vector2 dir)
        {
            _playerAnimation.Walk(dir);
        }

        private void DelayedFindPath()
        {
            if (transform.parent == null)
            {
                _playerAnimation.PauseAnimation = false;
                _walkToNode = _follower.FindClosest();
                _collider2D.isTrigger = false;
            }
        }

        private void FindPath(Node last)
        {
            Graph g = GameObject.Find("Nav").GetComponent<Graph>();

            List<Node> nodes = g.nodes;
            Node n1 = last != null ? last : nodes[Random.Range(0, nodes.Count)];
            Node n2 = nodes[Random.Range(0, nodes.Count)];

            _follower.Begin(n1, n2, this);
        }
    }
}