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
        
        public static int MaxAttraction = 100;
        public static int TotalAttraction = 0;

        [SerializeField] private Slider _catCount;
        [SerializeField] private Slider _attractionCount;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            TotalCat = Mathf.Clamp(TotalCat, 0, MaxCat);
            TotalAttraction = Mathf.Clamp(TotalAttraction, 0, MaxAttraction);
            
            _catCount.value = ((float) TotalCat )/ ((float) MaxCat);
            
            _attractionCount.value = ((float) TotalAttraction )/ ((float) MaxAttraction);
        }
    }
}