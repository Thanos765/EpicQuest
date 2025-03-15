using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinManager : MonoBehaviour
{
    public Boss boss1; // Reference to the first boss
    public Boss2 boss2; // Reference to the second boss
    public GameObject winScreenCanvas;

    private void Update()
    {

        if (boss1!=null && boss2!=null && boss1.isDefeated && boss2.isDefeated)
        {
            Debug.Log("both bosses are dead");
            // Activate the win screen canvas

            if (winScreenCanvas != null)
            {
                Debug.Log("winscreencanvas is active");
                winScreenCanvas.SetActive(true);
            }
            
        }
    }

    public void ExitGame()
    {
       SceneManager.LoadScene("menu");
        PlayerPrefs.DeleteKey("SavedScene");
        Debug.Log("quitting game");
    }
}
