using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

namespace CW.Scripts
{
    [RequireComponent(typeof(Camera2DFollow))]
    public class CameraManager : MonoBehaviour
    {

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