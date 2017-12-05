using System.Collections.Generic;
using CW.Scripts.UI;
using UnityEngine;

namespace CW.Scripts.Interactables
{
    public class Trash : Interactable
    {
        private Collider2D _collider2D;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }
        
        public override void Interact(Player player, Interactions interaction)
        {
            if (interaction == Interactions.Trash_Newspaper)
            {
                if (player.HasPickedUpInteractable() && !player.IsPickedUpInteractableMurderable())
                {
                    Interactable interactable = player.TakePickedupItem();
                    Destroy(interactable.gameObject);
                    CatMeter.TotalAttraction--;
                    CatMeter.TotalNewspapers--;

                    EvSys.Instance().AddMessage("Destroyed Newspaper : <color=green>-1 to Cat Attraction</color>");
                }
            }
            else if (interaction == Interactions.Pickup)
            {
                _collider2D.enabled = false;
            }
            else if (interaction == Interactions.Drop)
            {
                _collider2D.enabled = true;
                _collider2D.isTrigger = false;
            }
        }

        public override Dictionary<KeyCode, Interactions> InteractOptions(Player player)
        {
            Dictionary<KeyCode, Interactions> interactions =
                new Dictionary<KeyCode, Interactions> {{KeyCode.Alpha1, Interactions.Pickup}};


            if (player.HasPickedUpInteractable() && !player.IsPickedUpInteractableMurderable())
            {
                interactions.Add(KeyCode.Alpha2, Interactions.Trash_Newspaper);
            }

            return interactions;
        }

        public override bool IsAvailable()
        {
            return true;
        }

        public override void SetDirection(Vector2 walk)
        {
        }
    }
}