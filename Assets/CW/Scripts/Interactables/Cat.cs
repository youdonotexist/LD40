using System.Collections.Generic;
using UnityEngine;

namespace CW.Scripts.Interactables
{
	public class Cat : Interactable, IPathTracker
	{
		[SerializeField] private AudioClip _meowClip;

		private AudioSource _audioSource;
		private Collider2D _collider2D;
		private PlayerAnimation _playerAnimation;
		

		void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
			_collider2D = GetComponent<Collider2D>();
			_playerAnimation = GetComponent<PlayerAnimation>();
		}
	
		// Use this for initialization
		void Start ()
		{
			FindPath(null);
		}

		public override void Interact(Player _player, Interactions interaction)
		{
			if (interaction == Interactions.Pet)
			{
				_audioSource.PlayOneShot(_meowClip);
			}
			else if (interaction == Interactions.Pickup)
			{
				_collider2D.enabled = false;
			}
			else if (interaction == Interactions.Drop)
			{
				_collider2D.enabled = true;
			}
		}

		public override Dictionary<KeyCode, Interactions> InteractOptions(Player player)
		{
			return new Dictionary<KeyCode, Interactions>()
			{
				{KeyCode.Alpha1, Interactions.Pickup},
				{KeyCode.Alpha2, Interactions.Pet},
				{KeyCode.Alpha3, Interactions.Kill}
			};
		}

		public override bool IsAvailable()
		{
			return true;
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

		private void FindPath(Node last)
		{
			Graph g = GameObject.Find("Nav").GetComponent<Graph>();

			List<Node> nodes = g.nodes;
			Node n1 = last != null ? last : nodes[Random.Range(0, nodes.Count)];
			Node n2 = nodes[Random.Range(0, nodes.Count)];
			
			GetComponent<Follower>().Begin(n1, n2, this);
		}
	}
}
