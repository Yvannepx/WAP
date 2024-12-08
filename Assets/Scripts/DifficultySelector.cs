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

        // Set up button listeners for color selection (optional behavior)
        purpleButton.onClick.AddListener(() => SelectColor("Purple"));
        greenButton.onClick.AddListener(() => SelectColor("Green"));
        orangeButton.onClick.AddListener(() => SelectColor("Orange"));

        pistachioButton.onClick.AddListener(() => SelectColor("Pistachio"));
        crailButton.onClick.AddListener(() => SelectColor("Crail"));
        goblinButton.onClick.AddListener(() => SelectColor("Goblin"));

        pizazzButton.onClick.AddListener(() => SelectColor("Pizazz"));
        limeButton.onClick.AddListener(() => SelectColor("Lime"));
        mantisButton.onClick.AddListener(() => SelectColor("Mantis"));
        astralButton.onClick.AddListener(() => SelectColor("Astral"));
        vividVioletButton.onClick.AddListener(() => SelectColor("Vivid Violet"));
        amaranthButton.onClick.AddListener(() => SelectColor("Amaranth"));

        purpleButton.onClick.AddListener(() => StartGameWithColor("Purple"));
        greenButton.onClick.AddListener(() => StartGameWithColor("Green"));
        orangeButton.onClick.AddListener(() => StartGameWithColor("Orange"));

    }

    public void LoadLevelWithColor(string colorName)
    {
        GameData.SelectedColor = colorName; // Save the color
        Debug.Log($"Selected Color: {colorName}"); // Ensure this logs the correct color
        SceneManager.LoadScene("LVL1 GAME"); // Load the scene
    }


    void StartGameWithColor(string colorName)
    {
        // Save the selected color to GameData
        GameData.SelectedColor = colorName;
        // Load the level or update the GameController with the selected color
        gameController.SetTargetColor(colorName);
        LoadLevelWithColor(colorName);
        
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

    public void SelectColor(string colorName)
    {
        // You can add more functionality here to handle the color selection
        Debug.Log("Selected Color: " + colorName);
        // Example: Change game color, update UI, etc.
    }
}