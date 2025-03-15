using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Inventory.Model;




[System.Serializable]
public class SaveData
{
    
    public Vector3 playerPosition;
    public int coins;
    public SerializableDictionary<string, bool> chestsOpened;
    public SerializableDictionary<string, bool> enemiesKilled;
    public string savedSceneName;



        public SaveData ()
        {
          savedSceneName = "Forest";
         coins = CoinCount.coins;
         chestsOpened = new SerializableDictionary<string, bool>();
         enemiesKilled = new SerializableDictionary<string, bool>();
        }

    }



