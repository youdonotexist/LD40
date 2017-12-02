using System.Collections;
using System.Collections.Generic;
using CW.Scripts;
using UnityEngine;

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

	public override void Interact(Transform transform)
	{
		_audioSource.PlayOneShot(_meowClip);
	}
}
