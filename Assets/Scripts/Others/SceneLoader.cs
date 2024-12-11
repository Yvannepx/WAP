using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PickAColor()
    {
        SceneManager.LoadScene("PickAColor");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
