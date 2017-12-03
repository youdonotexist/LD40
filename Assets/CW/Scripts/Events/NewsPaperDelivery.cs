using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace CW.Scripts.Events
{
	public class NewsPaperDelivery : Event
	{

		[SerializeField]
		private AudioClip _bellClip;

		[SerializeField] private GameObject _newsPaper;
		[SerializeField] private CircleCollider2D _newsPaperSpawnPoint;
		
		// Use this for initialization
		void Start () {
			Audio.PlayOneShot(_bellClip);
			GameObject go = Instantiate(_newsPaper);
			Vector3 rand = (Random.insideUnitCircle * _newsPaperSpawnPoint.radius);
			
			go.transform.position = _newsPaperSpawnPoint.transform.position + rand;
		}

		public void SetSpawnPoint(CircleCollider2D spawn)
		{
			_newsPaperSpawnPoint = spawn;
		}
	
		// Update is called once per fram}}}e
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
