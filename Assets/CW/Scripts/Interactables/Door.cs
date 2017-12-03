using System.Collections.Generic;
using UnityEngine;

namespace CW.Scripts.Interactables
{
    public class Door : Interactable
    {
        private bool _isOpen = false;
        private Collider2D _collider2D;
        private Transform _transform;
        private SpriteRenderer _spriteRenderer;

        void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _transform = GetComponent<Transform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void Interact(Player player, Interactions interaction)
        {
            if (interaction == Interactions.Close || interaction == Interactions.Open)
            {
                Vector3 angles = _transform.localEulerAngles;
                if (_spriteRenderer.flipY)
                {
                    angles.z += _isOpen ? 90.0f : -90.0f;
                }
                else
                {
                    angles.z += _isOpen ? -90.0f : 90.0f;
                }
                
                _transform.localEulerAngles = angles;

                _isOpen = !_isOpen;
                Neighbor.Instance().RedoPath();
            }
            else if (interaction == Interactions.Drop)
            {
                if (!_isOpen)
                {
                    Interact(player, Interactions.Open);
                }
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
                interactions.Add(KeyCode.Alpha1, _isOpen ? Interactions.Close : Interactions.Open);
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