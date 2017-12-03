using System.Collections.Generic;
using CW.Scripts.Interactables;
using UnityEngine;

namespace CW.Scripts
{
    public class CatFactory : MonoBehaviour
    {
        [SerializeField]
        private List<Cat> catPrefabs = new List<Cat>();

        private static CatFactory _this;

        public static CatFactory Instance()
        {
            if (_this == null)
            {
                _this = GameObject.Find("CatFactory").GetComponent<CatFactory>();
            }

            return _this;
        }

        public Cat RandomCat()
        {
            return Instantiate(catPrefabs[Random.Range(0, catPrefabs.Count)]);
        }
    }
}