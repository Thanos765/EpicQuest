using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorinteract : MonoBehaviour
{
    public float distanceThreshold = 1f;
    private bool isPlayerNear = false;

    private GameObject player; // Reference to the player object

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void InteractButtonClicked()
    {
        if (isPlayerNear)
        {

            if (SceneManager.GetActiveScene().name == "Forest")
            {
                SceneManager.LoadScene("Shop");
            }
            else
            {
                SceneManager.LoadScene("Forest");
                player.transform.position = new Vector3(8.565812f, 4.059112f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the player object exists and the destination scene is loaded
        if (player != null && scene.name == "Shop")
        {
            player.transform.position = new Vector3(8.565812f, 4.059112f);
        }
    }
}
