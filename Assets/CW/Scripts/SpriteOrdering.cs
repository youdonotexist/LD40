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
		public bool DoColliderResize = false;
		public bool DoRendererAdjjstment = true;
	
		// Use this for initialization
		void Awake () {
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_boxCollider2D = GetComponent<BoxCollider2D>();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (DoRendererAdjjstment)
			{
				Camera cam = CameraManager.Get().GetCamera();
				CenterY = cam.WorldToScreenPoint(transform.position).y * -1;
				_spriteRenderer.sortingOrder = (int) CenterY;
			}

			if (DoColliderResize && _boxCollider2D != null)
			{
				Vector3 v = _spriteRenderer.bounds.size;
				_boxCollider2D.size = v;
			}
		}
	}
}
