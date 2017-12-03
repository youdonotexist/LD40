using System.Collections;
using System.Collections.Generic;
using CW.Scripts;
using UnityEngine;

public class Toilet : Interactable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Interact(Player player, Interactions interaction)
	{
		throw new System.NotImplementedException();
	}

	public override Dictionary<KeyCode, Interactions> InteractOptions(Player player)
	{
		throw new System.NotImplementedException();
	}

	public override bool IsAvailable()
	{
		throw new System.NotImplementedException();
	}

	public override void SetDirection(Vector2 walk)
	{
		throw new System.NotImplementedException();
	}
}
