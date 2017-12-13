using CW.Scripts.Events;
using CW.Scripts.Interactables;
using UniRx;
using UnityEngine;

namespace CW.Scripts
{
    public class Player : MonoBehaviour
    {
        public enum PlayerInteractionCode
        {
            PickUp
        }

        public bool IsMovable = true;
        public Vector2 LastWalk = Vector2.zero;

        private Interactable _pickedUpInteractable;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider2D;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (_pickedUpInteractable != null)
            {
                _pickedUpInteractable.SetDirection(LastWalk);
            }
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
                if (_pickedUpInteractable != null)
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
                _pickedUpInteractable.SetDirection(walk);
            }

            LastWalk = walk;
        }

        public bool HasPickedUpInteractable()
        {
            return _pickedUpInteractable != null;
        }

        public bool IsPickedUpInteractableMurderable()
        {
            return _pickedUpInteractable != null && _pickedUpInteractable is Cat;
        }

        public void AnswerPhone()
        {
            UIManager.Instance().ShowText(SpeechDialog.PhoneInteraction().DoOnCompleted(EnableMovement));
            IsMovable = false;
        }

        private void EnableMovement()
        {
            IsMovable = true;
            PhoneCallEvent phoneCallEvent = FindObjectOfType<PhoneCallEvent>();
            Destroy(phoneCallEvent.gameObject);
        }

        public void KillCat()
        {
            if (IsPickedUpInteractableMurderable())
            {
                Destroy(_pickedUpInteractable.gameObject);
                _pickedUpInteractable = null;
            }
        }

        public Interactable TakePickedupItem()
        {
            Interactable tmp = _pickedUpInteractable;
            _pickedUpInteractable.transform.parent = null;
            _pickedUpInteractable = null;
            return tmp;
        }

        public Vector2 PlayerSize()
        {
            return _spriteRenderer.sprite.bounds.size;
        }

        public Vector2 ColliderSize()
        {
            return _collider2D.bounds.size;
        }
    }
}