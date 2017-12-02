using UnityEngine;

namespace CW.Scripts.Events
{
	public class PhoneCallEvent : Event
	{

		[SerializeField]
		private AudioClip _audioClip;

		private float _duration = 0.0f;
		
		void Start ()
		{
			Audio.loop = true;
			Audio.clip = _audioClip;
			Audio.Play();

		}
	
		// Update is called once per frame
		void Update () {
			if (_duration > 5.0f)
			{
				Destroy(gameObject);
			}

			_duration += Time.deltaTime;
		}

		public string Message()
		{
			return "Your mother is calling.";
		}
	}
}
