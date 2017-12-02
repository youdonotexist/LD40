using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace CW.Scripts
{
	[ExecuteInEditMode]
	public class SpriteOrdering : MonoBehaviour
	{

		private SpriteRenderer _spriteRenderer;
		private BoxCollider2D _boxCollider2D;
		public float CenterY = 0.0f;
	
		// Use this for initialization
		void Awake () {
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_boxCollider2D = GetComponent<BoxCollider2D>();
		}
	
		// Update is called once per frame
		void Update ()
		{
			Camera cam = CameraManager.Get().GetCamera();
			CenterY = cam.WorldToScreenPoint(transform.position).y * -1;
			_spriteRenderer.sortingOrder = (int) CenterY;

			if (_boxCollider2D)
			{
				Vector3 v = _spriteRenderer.bounds.size;
				_boxCollider2D.size = v;
			}
		}
	}
}
