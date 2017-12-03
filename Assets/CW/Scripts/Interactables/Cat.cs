using System.Collections.Generic;
using UnityEngine;

namespace CW.Scripts.Interactables
{
	public class Cat : Interactable
	{
		[SerializeField] private AudioClip _meowClip;

		private AudioSource _audioSource;

		void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
		}
	
		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
			if (!_audioSource.isPlaying && Random.value > 0.6)
			{
				_audioSource.PlayOneShot(_meowClip);
			}
		}

		public override void Interact(Transform transform, KeyCode keycode)
		{
			if (keycode == KeyCode.B)
			{
				_audioSource.PlayOneShot(_meowClip);
			}
		}

		public override Dictionary<KeyCode, string> InteractOptions()
		{
			return new Dictionary<KeyCode, string>()
			{
				{KeyCode.A, "Pick Up"},
				{KeyCode.B, "Pet"},
				{KeyCode.C, "Kill"}
			};
		}
	}
}
