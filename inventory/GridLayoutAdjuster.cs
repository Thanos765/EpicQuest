using UnityEngine;
using UnityEngine.UI;

public class GridLayoutAdjuster : MonoBehaviour
{

    public float minSlotSize = 70f; // Set the minimum slot size you want
    public float maxSlotSize = 175f; // Set the maximum slot size you want
    public float minSpacing = 20f; // Set the minimum spacing you want
    public float maxSpacing = 100f; // Set the maximum spacing you want

    public GridLayoutGroup gridLayout;

    private void Start()
    {
        // Get the GridLayoutGroup component
        gridLayout = GetComponent<GridLayoutGroup>();

        // Adjust the cell size, padding, and spacing based on the screen resolution
        AdjustLayout();
    }

    private void AdjustLayout()
    {
        // Calculate the screen width and height
        float screenWidth = Screen.width;
         float screenHeight = Screen.height;

        // Define your target resolutions
        int targetWidth = 1920;
        int targetHeight = 1080;

        // Calculate the aspect ratio of the screen
        float currentAspectRatio = screenWidth / screenHeight;

        // Calculate the desired cell size and spacing
        float cellSize;
        float spacing;

        if (screenWidth < targetWidth && screenHeight < targetHeight)
        {
            // Screen resolution is smaller or equal to the target, use min cell size and max spacing
            cellSize = minSlotSize;
            spacing = maxSpacing;
        }
        else
        {
            // Screen resolution is larger than the target and not 16:9, use max cell size and min spacing
            cellSize = maxSlotSize;
            spacing = minSpacing;
        }

        // Set the cell size, padding, and spacing in the Grid Layout Group
        gridLayout.cellSize = new Vector2(cellSize, cellSize);
        gridLayout.spacing = new Vector2(spacing, spacing);
    }

}
