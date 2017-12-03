using CW.Scripts.Interactables;
using UnityEngine;

namespace CW.Scripts
{
    public class Player : MonoBehaviour
    {
        public enum PlayerInteractionCode
        {
            PickUp
        }

        private Interactable _pickedUpInteractable;
        private SpriteRenderer _spriteRenderer;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Interact(Interactable.Interactions code, Interactable interactable)
        {
            if (code == Interactable.Interactions.Pickup)
            {
                _pickedUpInteractable = interactable;
                interactable.transform.parent = transform;
                interactable.transform.localPosition = Vector2.zero;
            }
            else if (code == Interactable.Interactions.Drop)
            {
                if (_pickedUpInteractable != null && interactable is Door)
                {
                    Drop();
                }
            }
        }

        public void Drop()
        {
            if (_pickedUpInteractable != null)
            {
                Vector2 pos = _pickedUpInteractable.transform.position;
                pos.y = _spriteRenderer.bounds.min.y;
                _pickedUpInteractable.transform.position = pos;
                
                _pickedUpInteractable.transform.parent = null;
                _pickedUpInteractable.Interact(this, Interactable.Interactions.Drop);
                _pickedUpInteractable = null;
                
            }
        }

        public void SetDirection(Vector2 walk)
        {
            if (walk == Vector2.zero)
            {
                return;
            }
            
            if (_pickedUpInteractable != null)
            {
                _pickedUpInteractable.transform.localPosition = (Vector2.Scale(walk, new Vector2(0.1f, 0.1f)));
            }
        }

        public bool HasPickedUpInteractable()
        {
            return _pickedUpInteractable != null;
        }
    }
}