using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Inventory.Model;


public class CoinCount : MonoBehaviour, ISaveController
{

    public static int coins=0;
    public TMP_Text coinCount;
    public bool canBuy;

  void Awake()
    {
       coinCount = GetComponent<TMP_Text>();
    }
    
    public void LoadGameData(SaveData data)
    {
        coins = data.coins;
    }

    public void SaveGameData(ref SaveData data)
    {
       data.coins=coins;
    }

  

    // Update is called once per frame
   void Update(){
    coinCount.text= "Coins:" + coins;

    }

}
