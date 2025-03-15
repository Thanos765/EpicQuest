using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="BossHealthData")]
public class BossHealthSO :  ScriptableObject
{
        [SerializeField]
        private float _value;

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChange?.Invoke(_value);
            }

        }
        public event Action<float> OnValueChange;
    
        
}


    

