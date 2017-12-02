﻿using System;
using UniRx;
using UnityEngine;

namespace CW.Scripts
{
    public class PlayerControls : MonoBehaviour
    {
        [SerializeField] private Transform _controlled;

        private Rigidbody2D _rigidbody2D;

        public float Speed = 10.0f;

        private Vector3 _lastForward = Vector3.down;

        private bool _didInteract = false;

        private static PlayerControls _this;

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

            //Debug.Log("Walk dir: " + walk);

            _rigidbody2D.MovePosition(_rigidbody2D.position + (walk * Speed * Time.deltaTime));

            Vector2 newLook = new Vector2(
                1.0f * Mathf.Sign(walk.x),
                1.0f * Mathf.Sign(walk.y)).normalized;

            _lastForward = newLook;

            Debug.DrawLine(_controlled.transform.position,
                _controlled.transform.position + new Vector3(_lastForward.x, _lastForward.y, 0.0f));

            float didInteract = Input.GetAxisRaw("Jump");

            if (Math.Abs(didInteract) > Mathf.Epsilon)
            {
                if (_didInteract == false)
                {
                    // Call your event function here.
                    RaycastHit2D hit = Physics2D.Raycast(_controlled.transform.position, _lastForward, 10000.0f);
                    if (hit.collider != null)
                    {
                        Interactable interactable = hit.collider.GetComponent<Interactable>();
                        if (interactable != null)
                        {
                            interactable.Interact(_controlled);
                        }
                    }
                    _didInteract = true;
                }
            }
            if (Math.Abs(didInteract) < Mathf.Epsilon)
            {
                _didInteract = false;
            }
        }
    }
}