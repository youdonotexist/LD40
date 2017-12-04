using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace CW.Scripts
{
    public class PlayerControls : MonoBehaviour
    {
        public enum Direction
        {
            Idle = 0,
            Up = 1,
            Right = 2,
            Down = 3,
            Left = 4
        }

        public float Speed = 10.0f;

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Player _player;
        [SerializeField] private PlayerAnimation _playerAnimation;

        private Rigidbody2D _rigidbody2D;
        private Vector3 _lastForward = Vector3.down;
        private Interactable _lastInteractable;

        private static PlayerControls _this;

        public static PlayerControls Instance()
        {
            return _this ?? (_this = GameObject.Find("PlayerControls").GetComponent<PlayerControls>());
        }

        void Awake()
        {
            _rigidbody2D = _player.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!_player.isMovable)
            {
                return;
            }
            Vector2 walk = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _playerAnimation.Walk(walk);
            _player.SetDirection(walk);
            _rigidbody2D.MovePosition(_rigidbody2D.position + (walk * Speed * Time.deltaTime));

            if (walk != Vector2.zero)
            {
                _lastForward = walk;
            }

            if (_lastInteractable == null && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton10)))
            {
                _player.Drop();
            }

            DetectInteractable();
        }

        private void DetectInteractable()
        {
            RaycastHit2D hit = Physics2D.BoxCast(_player.transform.position, _player.GetComponent<Collider2D>().bounds.size, 0.0f, _lastForward,
                1.0f, _layerMask);
            //RaycastHit2D hit = Physics2D.Raycast(_player.transform.position, _lastForward, 1.0f, _layerMask);
            if (hit.collider != null)
            {
                Debug.Log("hit name: " + hit.collider.name);
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null && interactable.IsAvailable())
                {
                    Debug.Log("hit interactable");
                    if (interactable != _lastInteractable)
                    {
                        _lastInteractable = interactable;
                        UIManager.Instance().ShowOptionsMenu(_player, interactable);
                    }
                }
                else
                {
                    _lastInteractable = null;
                    UIManager.Instance().ShowOptionsMenu(null, null);
                }
            }
            else
            {
                _lastInteractable = null;
                UIManager.Instance().ShowOptionsMenu(null, null);
            }
        }
    }
}