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
        [SerializeField] private Slider _catLadyCalculation;
        [SerializeField] private Image _catLadyArrow;

        // Update is called once per frame
        void Update()
        {
            TotalCat = Mathf.Clamp(TotalCat, 0, MaxCat);
            TotalAttraction = Mathf.Clamp(TotalAttraction, 0, MaxAttraction);
            
            float catPct = ((float) TotalCat )/ ((float) MaxCat);
            float attractionPct = ((float) TotalAttraction) / ((float) MaxAttraction);

            _catCount.value = catPct;
            _attractionCount.value = attractionPct;

            float arrowUp = catPct > 0.5f ? 1.0f : -1.0f;

            Vector3 scale = _catLadyArrow.transform.localScale;
            scale.y = arrowUp;
            _catLadyArrow.transform.localScale = scale;

            _catLadyArrow.color = arrowUp > 0.0f ? Color.red : Color.green;

            _catLadyCalculation.value = Mathf.Clamp(_catLadyCalculation.value + (Time.deltaTime * 0.01f * arrowUp), 0.0f, 1.0f);

        }
    }
}