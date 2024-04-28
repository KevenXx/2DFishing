using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text fishCountText; // Assign this in the editor if possible
    private int fishCount = 0; // Make this private to encapsulate

    // Property to get and set the fish count safely
    public int FishCount
    {
        get { return fishCount; }
        set { fishCount = Mathf.Max(0, value); } // Ensures fish count never goes below 0
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find and update the fish count text
        FindAndUpdateFishCountText();
    }

    private void FindAndUpdateFishCountText()
    {
        var fishCountTextObj = GameObject.Find("FishCountText");
        if (fishCountTextObj != null)
        {
            fishCountText = fishCountTextObj.GetComponent<Text>();
            UpdateFishCountText();
        }
        else
        {
            Debug.LogError("Fish count Text object not found in the scene.");
        }
    }

    public void UpdateFishCountText()
    {
        if (fishCountText != null)
        {
            fishCountText.text = "Ujete ribe: " + FishCount;
        }
        else
        {
            Debug.LogError("Fish count Text component not assigned or not found.");
        }
    }

    public void FishCaught()
    {
        FishCount++; // Increment fish count using the property
        UpdateFishCountText();

        // Load the SampleScene when the fish count is a multiple of 8
        if (FishCount % 8 == 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void LoadSampleSceneDirectly()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
}