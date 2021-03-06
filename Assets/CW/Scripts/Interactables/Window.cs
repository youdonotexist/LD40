using System.Collections.Generic;
using UnityEngine;

namespace CW.Scripts.Interactables
{
	public class Window : Interactable
	{

		[SerializeField] private Transform _spawnLocation;
		
		private bool _isOpen = false;
		private SpriteRenderer _spriteRenderer;

		private float _spawnRate = 10.0f; //Every 10 seconds
		private float _spawnWait = 0.0f;

		[SerializeField]
		private Sprite _open;
		
		[SerializeField]
		private Sprite _closed;

		void Awake()
		{
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
					cat.transform.position = _spawnLocation.position;

					_spawnWait = 0.0f;
					
					EvSys.Instance().AddMessage("Window Cat: <color=red>+1 to Cat Lady</color>");
				}

				_spawnWait += Time.deltaTime;
			}
		}
	}
}
