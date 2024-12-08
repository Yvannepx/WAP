using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YouWinCanvasController : MonoBehaviour
{
    public GameController gameController;  // Reference to GameController
    public Button nextButton;  // Reference to the Next button

    void Start()
    {
        // Add listener to the next button
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    // This method is called when the Next button is clicked
    // Method that will handle the button click
    private void OnNextButtonClicked()
    {
        // go to the next level (Level 2)
        SceneManager.LoadScene("Level2"); // Make sure Level2 is in the build settings
        Debug.Log("Goes to Level 2");
    }
}