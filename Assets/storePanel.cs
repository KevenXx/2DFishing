using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour
{
    public GameObject storePanel;  // Assign your store panel in the inspector
    public GameManager gameManager;  // Reference to your GameManager script
    
    private void Start()
    {
        // Initially the store panel should be hidden
        storePanel.SetActive(false);
    }

    // Call this method to open the store panel
    public void OpenStore()
    {
        storePanel.SetActive(true);
        UpdateFishCountDisplay();
    }

    // Call this method to close the store panel
    public void CloseStore()
    {
        storePanel.SetActive(false);
    }

    // Update the fish count display in the store panel
    private void UpdateFishCountDisplay()
    {
        Text fishCountText = storePanel.transform.Find("FishCountText").GetComponent<Text>();
        if (fishCountText != null && gameManager != null)
        {
            fishCountText.text = "Ujete ribe: " + gameManager.FishCount;
        }
        else
        {
            Debug.LogError("One of the components is not set or found.");
        }
    }

    // You would also need a method to call when the player tries to buy an item
    public void BuyItem(int itemCost)
    {
        if(gameManager.FishCount >= itemCost)
        {
            gameManager.FishCount -= itemCost;
            // Add the item to the player's inventory here
            UpdateFishCountDisplay(); // Update the UI to reflect the new fish count
        }
        else
        {
            Debug.Log("Not enough fish to buy this item!");
            // Optionally, you can update the UI to inform the player they don't have enough fish
        }
    }
}