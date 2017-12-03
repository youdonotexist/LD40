using UnityEngine;

namespace CW.Scripts
{
    public class PlayerAnimation : MonoBehaviour
    {
        public enum Direction
        {
            Idle = 0,
            Up = 1,
            Right = 2,
            Down = 3,
            Left = 4
        }

        [SerializeField] private Animator _playerAnimator;

        private Direction _direction;

        private readonly string _directionTrigger = "Direction";
        private readonly string _speedTrigger = "Speed";

        public void Walk(Vector2 walk)
        {
            if (walk == Vector2.zero)
            {
                _playerAnimator.SetFloat(_speedTrigger, 0.0f);
            }
            else
            {
                _playerAnimator.SetFloat(_speedTrigger, 1.0f);
            }

            Direction direction = _direction;
            if (walk != Vector2.zero)
            {
                if (walk.x < 0)
                {
                    direction = Direction.Left;
                }
                else if (walk.x > 0)
                {
                    direction = Direction.Right;
                }
                else if (walk.y > 0)
                {
                    direction = Direction.Up;
                }
                else
                {
                    direction = Direction.Down;
                }
            }

            if (direction != _direction)
            {
                _direction = direction;
                _setDirection(_direction);
            }
        }

        public void _setDirection(Direction direction)
        {
            _playerAnimator.SetInteger(_directionTrigger, (int) direction);
        }
    }
}