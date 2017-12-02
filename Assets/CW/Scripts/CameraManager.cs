using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

namespace CW.Scripts
{
    [RequireComponent(typeof(Camera2DFollow))]
    public class CameraManager : MonoBehaviour
    {
        private Camera2DFollow _camera2DFollow;
        private Camera _camera;

        void Awake()
        {
            _camera2DFollow = GetComponent<Camera2DFollow>();
            _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        public Camera GetCamera()
        {
            return GetComponent<Camera>();
        }

        public static CameraManager Get()
        {
            GameObject go = GameObject.Find("Main Camera");
            return go == null ? null : go.GetComponent<CameraManager>();
        }
    }
}