using System.Collections;
using System.Collections.Generic;
using CW.Scripts.Events;
using CW.Scripts.UI;
using UnityEngine;

namespace CW.Scripts
{
    public class Phone : Interactable
    {
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public override void Interact(Player player, Interactions interaction)
        {
            if (interaction == Interactions.Answer)
            {
                player.AnswerPhone();
                CatMeter.MaxAttraction -= 3;
                EvSys.Instance().AddMessage("Answered Phone: <color=green>-3 to Cat Attraction</color>");
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
                if (FindObjectOfType<PhoneCallEvent>())
                {
                    interactions.Add(KeyCode.Alpha1, Interactions.Answer);
                }
            }

            return interactions;
        }

        public override bool IsAvailable()
        {
            if (FindObjectOfType<PhoneCallEvent>())
            {
                return true;
            }

            return false;
        }

        public override void SetDirection(Vector2 walk)
        {
            
        }
    }
}