using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;


public interface ISaveController 
{
   void LoadGameData(SaveData data);
    void SaveGameData(ref SaveData data);

}

