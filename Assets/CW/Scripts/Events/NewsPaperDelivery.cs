using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace CW.Scripts.Events
{
	public class NewsPaperDelivery : Event
	{

		[SerializeField]
		private AudioClip _bellClip;

		[SerializeField] private GameObject _newsPaper;
		[SerializeField] private Transform _newsPaperSpawnPoint;
		
		// Use this for initialization
		void Start () {
			Audio.PlayOneShot(_bellClip);
			Instantiate(_newsPaper, _newsPaperSpawnPoint);
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
