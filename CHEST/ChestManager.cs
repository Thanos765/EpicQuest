using UnityEngine;

public class ChestManager : MonoBehaviour
{
    private static ChestManager instance;

    public InteractableObject currentChest;

    public static ChestManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ChestManager>();
            }
            return instance;
        }
    }

    public void SetCurrentChest(InteractableObject chest)
    {
        currentChest = chest;
    }

    public void Interact()
    {
        if (currentChest != null)
        {
            currentChest.InteractButtonClicked();
        }
    }
}