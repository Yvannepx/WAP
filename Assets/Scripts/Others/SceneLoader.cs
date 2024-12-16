using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneLoader : MonoBehaviour
{
    public void PickAColor()
    {
        SceneManager.LoadScene("PickAColor");
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        Debug.Log("Exiting play mode in the Editor...");
        EditorApplication.isPlaying = false; // Exits play mode in the Editor
    #else
        Debug.Log("Game is quitting...");
        Application.Quit(); // Quits the application in a build
    #endif
    }

    //FOR PAUSE
    public Button pauseButton;
    public Image pauseBG;
    public Button homeButton;
    public Button resumeButton;
    public Button restartButton;

    // Pause the game and show the pause menu
    public void Pause()
    {
        Time.timeScale = 0f; // Stops the game time (pauses the game)
        // pauseMenu.SetActive(true); // Show the pause menu
        pauseBG.gameObject.SetActive(true);
        homeButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // Resume the game and hide the pause menu
    public void Resume()
    {
        Time.timeScale = 1f; // Resume the game time
        // pauseMenu.SetActive(false); // Hide the pause menu
        pauseBG.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }
}
