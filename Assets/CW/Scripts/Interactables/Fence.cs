using System.Collections.Generic;
using CW.Scripts.UI;
using UnityEngine;

namespace CW.Scripts.Interactables
{
	public class Fence : Interactable
	{

		[SerializeField] private Transform[] _dropSpots; 

		public override void Interact(Player player, Interactions interaction)
		{
			if (interaction == Interactions.Free_Cat)
			{
				if (player.HasPickedUpInteractable() && player.IsPickedUpInteractableMurderable())
				{
					Transform closest = ClosestDropSpot(player.transform);
					Interactable cat = player.TakePickedupItem();
					cat.transform.position = closest.position;
					CatMeter.TotalCat--;

					EvSys.Instance().AddMessage("Freed Cat: <color=green>-1 to Cats</color>");
				}
			}
			else if (interaction == Interactions.Scream_At_The_Neighbours)
			{
				EvSys.Instance().AddMessage("Screaming At The Neighbours: <color=green>+2 to Cat Attraction</color>");
				CatMeter.TotalAttraction += 2;
			}
		}

		public override Dictionary<KeyCode, Interactions> InteractOptions(Player player)
		{
			Dictionary<KeyCode, Interactions> interactions = new Dictionary<KeyCode, Interactions> ();

			if (player.HasPickedUpInteractable())
			{
				if (player.IsPickedUpInteractableMurderable())
				{
					interactions.Add(KeyCode.Alpha1, Interactions.Free_Cat);
				}
				else
				{
					interactions.Add(KeyCode.Alpha1, Interactions.Drop);
				}
			}
				
			interactions.Add(interactions.Count > 0 ? KeyCode.Alpha2 : KeyCode.Alpha1, Interactions.Scream_At_The_Neighbours);

			return interactions;
		}

		public override bool IsAvailable()
		{
			return true;
		}

		public override void SetDirection(Vector2 walk)
		{
			//throw new System.NotImplementedException();
		}

		private Transform ClosestDropSpot(Transform trans)
		{
			if (_dropSpots == null || _dropSpots.Length == 0)
			{
				return null;
			}
			
			Transform closest = _dropSpots[0];
			float d = Vector3.Distance(closest.position, trans.position);
			
			for(int i = 1; i < _dropSpots.Length; i++)
			{
				Transform spot = _dropSpots[i];
				float nd = Vector3.Distance(spot.position, trans.position);
				if (nd < d)
				{
					d = nd;
					closest = spot;
				}
			}

			return closest;
		}
	}
}
