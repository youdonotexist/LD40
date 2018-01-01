using System.Collections.Generic;
using UnityEngine;

namespace CW.Scripts.Interactables
{
    public class Television : Interactable
    {
        [SerializeField] private SpriteRenderer _screen;

        [SerializeField] private Sprite[] _screens;

        private float _time = 0.0f;
        private const float Switch = 3.0f;

        private bool isOn;

        void Update()
        {
            _screen.enabled = isOn;
            if (isOn)
            {
                _time += Time.deltaTime;
                if (_time > Switch)
                {
                    _screen.sprite = _screens[Random.Range(0, _screens.Length)];
                    _time = 0.0f;
                }
            }
        }

        public override void Interact(Player player, Interactions interaction)
        {
            if (interaction == Interactions.Turn_Off)
            {
                isOn = false;
            }
            else if (interaction == Interactions.Turn_On)
            {
                isOn = true;
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
                interactions.Add(KeyCode.Alpha1, isOn ? Interactions.Turn_Off : Interactions.Turn_On);
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