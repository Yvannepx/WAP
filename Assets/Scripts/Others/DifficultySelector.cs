using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    public static string SelectedColor;
    public GameController gameController; // Reference to GameController
    public GameObject difficultyPanel;
    public GameObject easyPanel;
    public GameObject mediumPanel;
    public GameObject hardPanel;

    // Difficulty Buttons
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    // Close Buttons for each panel
    public Button easyXButton;
    public Button mediumXButton;
    public Button hardXButton;

    // Color Buttons for each panel
    public Button purpleButton;
    public Button greenButton;
    public Button orangeButton;

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
        // Initially hide all panels
        difficultyPanel.SetActive(false);
        easyPanel.SetActive(false);
        mediumPanel.SetActive(false);
        hardPanel.SetActive(false);

        // Set up button listeners for difficulty selection
        easyButton.onClick.AddListener(ShowEasyPanel);
        mediumButton.onClick.AddListener(ShowMediumPanel);
        hardButton.onClick.AddListener(ShowHardPanel);

        // Set up button listeners for panel closing
        easyXButton.onClick.AddListener(HideEasyPanel);
        mediumXButton.onClick.AddListener(HideMediumPanel);
        hardXButton.onClick.AddListener(HideHardPanel);

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
        
        // Optionally hide all panels
        easyPanel.SetActive(false);
        mediumPanel.SetActive(false);
        hardPanel.SetActive(false);
    }


    public void ShowDifficultyPanel()
    {
        difficultyPanel.SetActive(true);
    }
    public void HideDifficultyPanel()
    {
        difficultyPanel.SetActive(false);
    }

    public void ShowEasyPanel()
    {
        HideDifficultyPanel();
        easyPanel.SetActive(true);
        mediumPanel.SetActive(false);
        hardPanel.SetActive(false);
    }

    public void ShowMediumPanel()
    {
        HideDifficultyPanel();
        mediumPanel.SetActive(true);
        easyPanel.SetActive(false);
        hardPanel.SetActive(false);
    }

    public void ShowHardPanel()
    {
        HideDifficultyPanel();
        hardPanel.SetActive(true);
        easyPanel.SetActive(false);
        mediumPanel.SetActive(false);
    }

    public void HideEasyPanel()
    {
        easyPanel.SetActive(false);
    }

    public void HideMediumPanel()
    {
        mediumPanel.SetActive(false);
    }

    public void HideHardPanel()
    {
        hardPanel.SetActive(false);
    }
}