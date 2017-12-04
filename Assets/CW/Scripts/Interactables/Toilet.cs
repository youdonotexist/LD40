using System.Collections.Generic;
using CW.Scripts.UI;
using UnityEngine;

namespace CW.Scripts.Interactables
{
	public class Toilet : Interactable
	{
		[SerializeField]
		private Sprite _closed;
		
		[SerializeField]
		private Sprite _open;
		
		[SerializeField]
		private AudioClip _flush;
		
		private bool _isOpen = false;
		private Collider2D _collider2D;
		private Transform _transform;
		private SpriteRenderer _spriteRenderer;
		private AudioSource _audioSource;

		private void Awake()
		{
			_collider2D = GetComponent<Collider2D>();
			_transform = GetComponent<Transform>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_audioSource = GetComponent<AudioSource>();
		}

		public override void Interact(Player player, Interactions interaction)
		{
			if (interaction == Interactions.Close || interaction == Interactions.Open)
			{
				_spriteRenderer.sprite = interaction == Interactions.Close ? _closed : _open;
				
				_isOpen = !_isOpen;
				
			}
			else if (interaction == Interactions.Drop)
			{
				//do nothing
			}
			else if (interaction == Interactions.Flush)
			{
				//Play sound
				_audioSource.PlayOneShot(_flush);
			}
			else if (interaction == Interactions.Flush_Cat)
			{
				if (player.IsPickedUpInteractableMurderable())
				{
					_audioSource.PlayOneShot(_flush);
					player.KillCat();
					EvSys.Instance().AddMessage("Flushed Cat: <color=green> -1 to Cats</color>");
				}
			}
		}

		public override Dictionary<KeyCode, Interactions> InteractOptions(Player player)
		{
			Dictionary<KeyCode, Interactions> interactions = new Dictionary<KeyCode, Interactions>();

			if (player.HasPickedUpInteractable())
			{
				interactions.Add(KeyCode.Alpha1, Interactions.Drop);	
				
				if (player.IsPickedUpInteractableMurderable() && _isOpen)
				{
					interactions.Add(KeyCode.Alpha2, Interactions.Flush_Cat);
				}
				
			}
			else
			{
				interactions.Add(KeyCode.Alpha1, _isOpen ? Interactions.Close : Interactions.Open);
				interactions.Add(KeyCode.Alpha2, Interactions.Flush);
			}

			return interactions;
		}

		public override bool IsAvailable()
		{
			return true;
		}

		public override void SetDirection(Vector2 walk)
		{
			
		}
	}
}
