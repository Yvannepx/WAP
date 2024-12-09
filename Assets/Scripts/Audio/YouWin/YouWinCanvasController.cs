using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YouWinCanvasController : MonoBehaviour
{
    public GameController gameController;  // Reference to GameController
    public Button nextButton;  // Reference to the Next button

    public DifficultySelector difficultySelector;

    void Start()
    {
        
    }

    public void WAPMainMenu()
    {
        // Clear all PlayerPrefs data (optional: reset specific keys instead if needed)
        

    }
}
