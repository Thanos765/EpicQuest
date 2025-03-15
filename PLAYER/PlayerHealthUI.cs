using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Model
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField]
        private Slider HealthBar;

        [SerializeField]
        private FloatValueSO floatValue;

        private void OnEnable()
        {
           floatValue.OnValueChange += SetValue; 
        }

        private void OnDisable()
        {
            floatValue.OnValueChange -= SetValue;
        }

        public void SetValue(float currentValue)
        {
            HealthBar.value = currentValue;
            
        }
    }
}
