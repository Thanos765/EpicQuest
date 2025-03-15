using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Teleport : MonoBehaviour
{

    public string nextSceneName;
    public float distanceThreshold = 1f;
    public string bossSceneName = "Boss";


    private bool isPlayerNear = false;
    //when you press enter it teleports

    public void TeleportButtonClicked()
    {
        if (isPlayerNear)
        {
            SceneManager.LoadSceneAsync(nextSceneName);
            SaveController.instance.SaveGame();
            string currentScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("SavedScene", currentScene);
        }
    }



    //if the player is near teleport when he presses enter
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }
    //if the player is not near do nothing
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
    // range of the teleport
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceThreshold);
    }
}


