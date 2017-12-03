using UnityEngine;

namespace CW.Scripts
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _playerAnimator;

        private PlayerControls.Direction _direction;

        private readonly string _directionTrigger = "Direction";
        private readonly string _speedTrigger = "Speed";

        public bool PauseAnimation = false;

        public void Walk(Vector2 walk)
        {
            if (walk == Vector2.zero || PauseAnimation)
            {
                _playerAnimator.SetFloat(_speedTrigger, 0.0f);
            }
            else
            {
                _playerAnimator.SetFloat(_speedTrigger, 1.0f);
            }

            PlayerControls.Direction direction = _direction;
            if (walk != Vector2.zero)
            {
                float absX = Mathf.Abs(walk.x);
                float absY = Mathf.Abs(walk.y);

                if (absX > absY)
                {
                    if (walk.x < 0)
                    {
                        direction = PlayerControls.Direction.Left;
                    }
                    else if (walk.x > 0)
                    {
                        direction = PlayerControls.Direction.Right;
                    }
                }
                else
                {
                    if (walk.y > 0)
                    {
                        direction = PlayerControls.Direction.Up;
                    }
                    else
                    {
                        direction = PlayerControls.Direction.Down;
                    }    
                }
            }

            if (direction != _direction)
            {
                _direction = direction;
                _setDirection(_direction);
            }
        }

        public void _setDirection(PlayerControls.Direction direction)
        {
            _playerAnimator.SetInteger(_directionTrigger, (int) direction);
        }
    }
}