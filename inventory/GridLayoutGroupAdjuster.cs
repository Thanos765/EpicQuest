using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutGroupAdjuster : MonoBehaviour
{

    public float minSlotSize = 70f; // Set the minimum slot size you want
    public float maxSlotSize = 150f; // Set the maximum slot size you want
    public float minSpacing = 20f; // Set the minimum spacing you want
    public float maxSpacing = 100f; // Set the maximum spacing you want
   private float  maxSlotSize2 = 180f; //when resolution gets too high set slot size to this

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

        //really high resolution
        int maxTargetWidth = 3200; 
        int maxTargetHeight = 1440;

        // Calculate the aspect ratio of the screen
        float currentAspectRatio = screenWidth / screenHeight;

        // Calculate the desired cell size and spacing
        float slotSize;
        float spacing;

        if (screenWidth <= targetWidth && screenHeight <= targetHeight)
        {
            // Screen resolution is smaller or equal to the target, use min cell size and max spacing
            slotSize = minSlotSize;
            spacing = maxSpacing;
        }
        //when resolution gets higher than 3200x1440
        else if (screenWidth >= maxTargetWidth && screenHeight >= maxTargetHeight){
                slotSize = maxSlotSize2;
                spacing = minSpacing;
        }
        //when resolution is higher than 1920x1080 and smaller than 3200x1440
        else
        {
            // Screen resolution is larger than the target, use max cell size and min spacing
            slotSize = maxSlotSize;
            spacing = minSpacing;
        }

        // Set the cell size, padding, and spacing in the Grid Layout Group
        gridLayout.cellSize = new Vector2(slotSize, slotSize);
        gridLayout.spacing = new Vector2(spacing, spacing);
    }

}