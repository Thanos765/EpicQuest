using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   public static bool Gameispaused = false;
  
   public GameObject PauseMenuUI; 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Gameispaused) {
                Resume();
            
            }else
            {
                Pause();
            }
        }
    }

    public void Resume()
     {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Gameispaused = false;
    }
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Gameispaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
         SaveController.instance.SaveGame();
    }
    public void QuitGame()
    {
        Debug.Log("quitting game..");
        Application.Quit();
    }
  
}
