using UniRx;
using UnityEngine;

namespace CW.Scripts
{
    [ExecuteInEditMode]
    public class SpriteOrderRelative : MonoBehaviour
    {
        private SpriteOrdering _spriteOrdering;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _spriteOrdering = GetComponentInParent<SpriteOrdering>();
            _renderer = GetComponent<SpriteRenderer>();

            _spriteOrdering.CenterY
                .Do(order => { _renderer.sortingOrder = ((int) order) + 1; }).Subscribe();
        }
    }
}