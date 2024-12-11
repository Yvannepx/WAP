using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    public static string SelectedColor;
    public GameControllerLEVEL3REAL gameController; // Reference to GameController

    // Color Buttons for each panel
    public Button purpleButton;
    public Button orangeButton;
    public Button greenButton;

    public Button pistachioButton;
    public Button crailButton;
    public Button goblinButton;

    public Button pizazzButton;
    public Button limeButton;
    public Button mantisButton;
    public Button astralButton;
    public Button vividVioletButton;
    public Button amaranthButton;

    void Start()
    {

        // Assuming you have buttons set up for each output color (Pistachio, Crail, etc.)
        purpleButton.onClick.AddListener(() => StartGameWithColor("Purple"));
        orangeButton.onClick.AddListener(() => StartGameWithColor("Orange"));
        greenButton.onClick.AddListener(() => StartGameWithColor("Green"));

        pistachioButton.onClick.AddListener(() => StartGameWithColor("Pistachio"));
        crailButton.onClick.AddListener(() => StartGameWithColor("Crail"));
        goblinButton.onClick.AddListener(() => StartGameWithColor("Goblin"));
        
        pizazzButton.onClick.AddListener(() => StartGameWithColor("Pizazz"));
        limeButton.onClick.AddListener(() => StartGameWithColor("Lime"));
        mantisButton.onClick.AddListener(() => StartGameWithColor("Mantis"));
        astralButton.onClick.AddListener(() => StartGameWithColor("Astral"));
        vividVioletButton.onClick.AddListener(() => StartGameWithColor("Vivid Violet"));
        amaranthButton.onClick.AddListener(() => StartGameWithColor("Amaranth"));
    }

    public void LoadLevelWithColor(string colorName)
    {
        SceneManager.LoadScene("LVL1 GAME");
    }

    public void LoadLevelWithColor2(string colorName)
    {
        SceneManager.LoadScene("LVL2 GAME");
    }

    public void LoadLevelWithColor3(string colorName)
    {
        SceneManager.LoadScene("LVL3 GAME");
    }

    public void StartGameWithColor(string colorName)
    {
        Debug.Log("Selected Color: " + colorName);
        // Save the selected color to GameData
        GameData.SelectedColor = colorName;
        // Load the level or update the GameController with the selected color
        gameController.SetTargetColor(colorName);
        
    }

}