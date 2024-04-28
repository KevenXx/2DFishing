using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTrigger : MonoBehaviour
{
    public StorePanel storePanelScript;  // Assign in the inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Assuming your player GameObject has the tag "Player"
        {
            storePanelScript.OpenStore();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            storePanelScript.CloseStore();
        }
    }
}