using UnityEngine;
using UnityEngine.UI;

namespace CW.Scripts.UI
{
    public class CatMeter : MonoBehaviour
    {
        public static int MaxCat = 30;
        public static int TotalCat = 0;
        public static int MaxNewspapers = 30;
        public static int TotalNewspapers = 0;

        [SerializeField] private Slider _catCount;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            _catCount.value = ((float) TotalCat )/ ((float) MaxCat);
        }
    }
}