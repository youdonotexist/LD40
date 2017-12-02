using UnityEngine;

namespace CW.Scripts.Events
{
	public class NewsPaperDelivery : Event
	{

		[SerializeField]
		private AudioClip _bellClip;
		
		// Use this for initialization
		void Start () {
			Audio.PlayOneShot(_bellClip);
			
		}
	
		// Update is called once per frame
		void Update () {
			if (!Audio.isPlaying)
			{
				Destroy(gameObject);
			}
		}

		public string Message()
		{
			return "Newspaper Delivery!!";
		}
	}
}
