using System.Collections.Generic;
using UnityEngine;

namespace CW.Scripts.Interactables
{
	public class Window : Interactable {

		private bool _isOpen = false;
		private Collider2D _collider2D;
		private Transform _transform;
		private SpriteRenderer _spriteRenderer;

		private float _spawnRate = 10.0f; //Every 10 seconds
		private float _spawnWait = 0.0f;

		[SerializeField]
		private Sprite _open;
		
		[SerializeField]
		private Sprite _closed;

		void Awake()
		{
			_collider2D = GetComponent<Collider2D>();
			_transform = GetComponent<Transform>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}
		
		public override void Interact(Player player, Interactions interaction)
		{
			if (interaction == Interactions.Close)
			{
				_spriteRenderer.sprite = _closed;
				_isOpen = false;
			}
			else if (interaction == Interactions.Open)
			{
				_spriteRenderer.sprite = _open;
				_isOpen = true;
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
				interactions.Add(KeyCode.Alpha1, _isOpen ? Interactions.Close : Interactions.Open);	
			}

			return interactions;
		}

		public override bool IsAvailable()
		{
			return true;
		}

		public override void SetDirection(Vector2 walk)
		{
			throw new System.NotImplementedException();
		}

		void Update()
		{
			if (_isOpen)
			{
				if (_spawnWait > _spawnRate)
				{
					Cat cat = CatFactory.Instance().RandomCat();
					Vector2 pos = _spriteRenderer.bounds.min;
					cat.transform.position = pos;

					_spawnWait = 0.0f;
				}

				_spawnWait += Time.deltaTime;
			}
		}
	}
}
