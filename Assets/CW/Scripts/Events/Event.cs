using UnityEngine;

namespace CW.Scripts.Events
{
	public class Event : MonoBehaviour
	{

		protected AudioSource Audio;

		protected virtual void Awake()
		{
			Audio = GetComponent<AudioSource>();
		} 
		
		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
		
		}
	}
}
