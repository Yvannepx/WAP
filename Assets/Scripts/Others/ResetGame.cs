using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetGame : MonoBehaviour
{
    public void ResetGameScenePickAColor()
    {
        Time.timeScale = 1f;
        // Reload the current scene by using the active scene index
        SceneManager.LoadScene("PickAColor");
    }
    // This function resets the current scene when called
    public void ResetGameScene()
    {
        Time.timeScale = 1f;
        // Reload the current scene by using the active scene index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ResetGameSceneMM()
    {
        Time.timeScale = 1f;
        // Reload the current scene by using the active scene index
        SceneManager.LoadScene("WAPMainMenu");
    }
    

    public Button[] buttons;  // Reference to all buttons in the scene

    void Start()
    {
        // Loop through all buttons and add the listener for each
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    // This function will be called for every button click
    void OnButtonClick()
    {
        // Play the button click SFX via AudioManager
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("buttonclick");
        }
        else
        {
            Debug.LogWarning("AudioManager is not found!");
        }
    }
}
