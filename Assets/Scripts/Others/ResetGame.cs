using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    // This function resets the current scene when called
    public void ResetGameScene()
    {
        Time.timeScale = 1f;
        // Reload the current scene by using the active scene index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ResetGameScenePickAColor()
    {
        Time.timeScale = 1f;
        // Reload the current scene by using the active scene index
        SceneManager.LoadScene("PickAColor");
    }
    public void ResetGameSceneMM()
    {
        Time.timeScale = 1f;
        // Reload the current scene by using the active scene index
        SceneManager.LoadScene("WAPMainMenu");
    }
}
