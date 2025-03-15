using Inventory.Model;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    // when you click play, scene switches to gameScene
    public void PlayGame(){
        SaveController.instance.NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.DeleteKey("SavedScene");
        CoinCount.coins = 0;

    }

    public void Options(){

    }

    public void Quit(){
        Debug.Log("quit!!!");
        Application.Quit();
    }


       public void Load()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string levelToLoad = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            Debug.Log("No saved scene found. You need to save the game first.");
        }
    }
}


