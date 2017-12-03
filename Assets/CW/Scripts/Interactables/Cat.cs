using System.Collections.Generic;
using UnityEngine;

namespace CW.Scripts.Interactables
{
	public class Cat : Interactable
	{
		[SerializeField] private AudioClip _meowClip;

		private AudioSource _audioSource;
		private Collider2D _collider2D;

		void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
			_collider2D = GetComponent<Collider2D>();
		}
	
		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
			
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
	}
}
