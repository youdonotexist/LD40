using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace CW.Scripts
{
    public class PlayerControls : MonoBehaviour
    {
        [SerializeField] private Transform _controlled;
        [SerializeField] private PlayerAnimation _playerAnimation;

        private Rigidbody2D _rigidbody2D;

        public float Speed = 10.0f;

        private Vector3 _lastForward = Vector3.down;

        private bool _didInteract = false;

        private static PlayerControls _this;

        private Interactable _lastInteractable;

        public static PlayerControls Instance()
        {
            return _this ?? (_this = GameObject.Find("PlayerControls").GetComponent<PlayerControls>());
        }

        void Awake()
        {
            _rigidbody2D = _controlled.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 walk = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _playerAnimation.Walk(walk);

            //Debug.Log("Walk dir: " + walk);

            _rigidbody2D.MovePosition(_rigidbody2D.position + (walk * Speed * Time.deltaTime));

            if (walk != Vector2.zero)
            {
                _lastForward = walk;
            }

            Debug.DrawLine(_controlled.transform.position,
                _controlled.transform.position + new Vector3(_lastForward.x, _lastForward.y, 0.0f));

            RaycastHit2D hit = Physics2D.Raycast(_controlled.transform.position, _lastForward, 1.0f);
            if (hit.collider != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                Player player = _controlled.GetComponent<Player>();
                if (interactable != null && interactable != _lastInteractable)
                {
                    UiManager.Instance().ShowOptionsMenu(player, interactable);
                }
                else
                {
                    UiManager.Instance().ShowOptionsMenu(null, null);
                }
            }
            else
            {
                UiManager.Instance().ShowOptionsMenu(null, null);
            }
            _didInteract = true;
        }
    }
}