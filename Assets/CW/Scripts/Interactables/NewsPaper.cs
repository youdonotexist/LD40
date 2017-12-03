using System.Collections;
using System.Collections.Generic;
using CW.Scripts;
using CW.Scripts.UI;
using UnityEngine;

namespace CW.Scripts
{
    public class NewsPaper : Interactable
    {
        private Collider2D _collider2D;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        void Start()
        {
            CatMeter.TotalNewspapers++;
        }

        public override void Interact(Player player, Interactions interaction)
        {
            if (interaction == Interactions.Pickup)
            {
                _collider2D.enabled = false;
            }
            else if (interaction == Interactions.Drop)
            {
                _collider2D.enabled = true;
            }
        }

        public override Dictionary<KeyCode, Interactions> InteractOptions(Player player)
        {
            Dictionary<KeyCode, Interactions> interactions = new Dictionary<KeyCode, Interactions>();
            if (player.HasPickedUpInteractable())
            {
                interactions.Add(KeyCode.Alpha1, Interactions.Drop);
            }
            else
            {
                interactions.Add(KeyCode.Alpha1, Interactions.Pickup);
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

        private void OnDestroy()
        {
            CatMeter.TotalNewspapers--;
        }
    }
}