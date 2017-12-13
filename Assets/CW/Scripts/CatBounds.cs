using CW.Scripts.Interactables;
using CW.Scripts.UI;
using UnityEngine;

namespace CW.Scripts
{
    public class CatBounds : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("On trig enter: " + other);
            if (other.GetComponent<Cat>() != null)
            {
                CatMeter.TotalCat++;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("On trig exit: " + other);
            
            if (other.GetComponent<Cat>() != null)
            {
                CatMeter.TotalCat--;
            }
        }
    }
}