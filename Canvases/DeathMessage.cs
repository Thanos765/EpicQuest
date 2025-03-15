using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMessage : MonoBehaviour
{

    public GameObject player;
    public GameObject DeathScreenCanvas;

    private void Update()
    {

        if (player ==null)
        {
            Debug.Log("you died");
            // Activate the win screen canvas

            if (DeathScreenCanvas != null)
            {
                Debug.Log("Deathscreencanvas is active");
                DeathScreenCanvas.SetActive(true);
            }
            
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("menu");
        PlayerPrefs.DeleteKey("SavedScene");
        Debug.Log("quitting game");
    }
}
