using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    
        [SerializeField]
        private Slider DarkWizardHealthBar;

        [SerializeField]
        private BossHealthSO floatValue;

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
           DarkWizardHealthBar.value = currentValue;
        }
    }

