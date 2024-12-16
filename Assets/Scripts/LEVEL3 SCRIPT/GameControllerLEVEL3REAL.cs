using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;

public class GameControllerLEVEL3REAL : MonoBehaviour
{

    public int countdownTime; // Set this in the Inspector (e.g., 3 for a 3-second countdown)
    public Text countdownDisplay; // Reference the Text object in your scene
    public GameController gameController;
    public Image darkPanel;

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString(); // Display the countdown number
            yield return new WaitForSeconds(1f); // Wait for 1 second
            countdownTime--; // Decrease the countdown number
        }
        darkPanel.gameObject.SetActive(false);

        // Once the countdown reaches 0
        countdownDisplay.text = "GO!"; // Display "GO!"
        yield return new WaitForSeconds(1f); // Wait for a second before hiding the text

        countdownDisplay.gameObject.SetActive(false); // Hide the countdown display
    }
    private bool hasPlayedWinSound = false;
    private bool hasPlayedLoseSound = false;
    private bool hasPlayedLoseHalfSound = false;
    public MyMovementTouchpad touchpadController;
    public Image WinBG;
    public Image WinglowAnim;
    public Image WintrophyAnim;
    public Image LoseBG;
    public Image LosetrophyAnim;
    public Image LosetrophyAnimHALF;
    public Button nextLvlButton;
    public Button tryAgainBtn;
    public Button LosemenuButton;
    public SpriteRenderer hammer;

    // Reference to the potion, pipe, and light prefabs and spawn points
    public GameObject[] potionPrefabs;
    public GameObject[] pipePrefabs;
    public Transform[] potionSpawnPoints;
    public Transform[] pipeSpawnPoints;
    public GameObject[] lightPrefabs;
    public Transform[] lightSpawnPoints;

    // UI elements to display target color and game time
    public TMP_Text targetColorText;
    public TMP_Text resultText;
    public Image targetColorImage;
    public Sprite[] targetColorImages;

    // Game time and text for the timer
    public float gameTime = 5f;
    public TMP_Text gameText;

    // A list that tracks the remaining potions and their colors
    private List<string> potionsRemaining = new List<string>();

    // Color combinations needed to form the target colors (e.g., Pistachio = Orange + Green)
    private Dictionary<string, List<string[]>> colorCombinations = new Dictionary<string, List<string[]>>
    {
        { "Pizazz", new List<string[]> { new string[] { "Red-Orange", "Yellow-Orange" } } }, // ff8e00
        { "Lime", new List<string[]> { new string[] { "Yellow-Orange", "Yellow-Green" } } }, // d6eb18
        { "Mantis", new List<string[]> { new string[] { "Yellow-Green", "Blue-Green" } } }, // 57c058
        { "Astral", new List<string[]> { new string[] { "Blue-Green", "Blue-Purple" } } }, // 356da7
        { "Vivid Violet", new List<string[]> { new string[] { "Blue-Purple", "Red-Purple" } } }, // 9938a9
        { "Amaranth", new List<string[]> { new string[] { "Red-Purple", "Red-Orange" } } } // e32d43
    };


    // List of other basic potion colors
    private List<string> otherColors = new List<string> 
    { 
        "Red-Orange", 
        "Yellow-Orange", 
        "Yellow-Green", 
        "Blue-Green", 
        "Blue-Purple", 
        "Red-Purple" 
    };


    // Tracking the potions that have been smashed and those still remaining
    public List<string> smashedPotions = new List<string>();
    public List<string> unsmashedPotions = new List<string>();

    // Target color for the current round
    public string targetColor;
    public HashSet<string> passedColors = new HashSet<string>(); // Track passed target colors

    private void Start()
    {
        AudioManager.Instance.PlayMusic("levels");
        StartCoroutine(CountdownToStart());
        
        // Check if a color is selected in GameData (it should be set in DifficultySelector)
        if (!string.IsNullOrEmpty(GameData.SelectedColor))
        {
            string selectedColor = GameData.SelectedColor;

            // Set the target color and update the UI
            SetTargetColor(selectedColor);

            // Randomize spawn points for potions, pipes, and lights
            RandomizeSpawnPoints();
            SpawnPotions();
            SpawnPipes();
            SpawnLights();

            // Initialize the list of unsmashed potions
            unsmashedPotions = new List<string>(potionsRemaining);
        }
        else
        {
            Debug.LogError("No color selected! Make sure the color is set in DifficultySelector.");
        }
    }

    private void Update()
    {
        // Decrease the game timer each frame
        gameTime -= Time.deltaTime;

        if (gameTime >= 8) {
            touchpadController.OnMouseUp();
        }
        
        // End the game if time runs out
        if (gameTime <= 0)
        {
            gameTime = 0;
            EndGame();
            touchpadController.istouchpadactive = false; 
            touchpadController.OnMouseUp();
        }

        // Update the timer text
        gameText.text = Mathf.CeilToInt(gameTime).ToString();
    }

    public void SetTargetColor(string color)
    {
        targetColor = color;
        targetColorText.text = $"{targetColor}"; // Update the text UI
        SetTargetColorImage(targetColor); // Update the image UI
        Debug.Log($"Game started with target color: {targetColor}");
    }

    // Set the sprite for the target color
    public void SetTargetColorImage(string color)
    {
        Sprite targetSprite = color switch
        {
            "Pizazz" => targetColorImages[0],   // Red-Orange + Yellow-Orange
            "Lime" => targetColorImages[1],     // Yellow-Orange + Yellow-Green
            "Mantis" => targetColorImages[2],   // Yellow-Green + Blue-Green
            "Astral" => targetColorImages[3],   // Blue-Green + Blue-Purple
            "Vivid Violet" => targetColorImages[4], // Blue-Purple + Red-Purple
            "Amaranth" => targetColorImages[5], // Red-Purple + Red-Orange
            _ => null
        };


        if (targetSprite != null)
            targetColorImage.sprite = targetSprite;
        else
            Debug.LogWarning($"No sprite found for {color}");
    }

    // Randomize the spawn points for the potions, pipes, and lights
    private void RandomizeSpawnPoints()
    {
        List<int> indices = Enumerable.Range(0, potionSpawnPoints.Length).ToList();
        indices = indices.OrderBy(x => Random.value).ToList();
        potionSpawnPoints = indices.Select(i => potionSpawnPoints[i]).ToArray();
        pipeSpawnPoints = indices.Select(i => pipeSpawnPoints[i]).ToArray();
        lightSpawnPoints = indices.Select(i => lightSpawnPoints[i]).ToArray();
    }

    // Spawn the potions at the spawn points
    private void SpawnPotions()
    {
        List<string> colorsToSpawn = new List<string>(otherColors);
        List<string> correctColors = GetCorrectPotionCombinations(targetColor);

        // Ensure that the required colors for the target are included
        while (!correctColors.All(color => colorsToSpawn.Contains(color)))
        {
            colorsToSpawn = colorsToSpawn.OrderBy(x => Random.value).Distinct().Take(3).ToList();
            if (!correctColors.All(color => colorsToSpawn.Contains(color)))
                colorsToSpawn.Clear();
        }

        // Instantiate potions at the spawn points
        for (int i = 0; i < potionSpawnPoints.Length; i++)
        {
            string color = colorsToSpawn[i];
            potionsRemaining.Add(color);

            GameObject potionPrefab = GetPotionPrefabByColor(color);
            GameObject potionInstance = Instantiate(potionPrefab, potionSpawnPoints[i].position, Quaternion.identity);

            if (potionInstance.TryGetComponent(out PotionBehaviorLEVEL3REAL potionBehavior))
            {
                potionBehavior.potionColor = color;
                potionBehavior.gameController = this;
            }
        }
    }

    // Spawn the pipes at the corresponding spawn points
    private void SpawnPipes()
    {
        for (int i = 0; i < pipeSpawnPoints.Length; i++)
        {
            if (i < potionsRemaining.Count && i < pipePrefabs.Length)
            {
                string potionColor = potionsRemaining[i];
                GameObject pipePrefab = GetPipePrefabByColor(potionColor);

                if (pipePrefab != null)
                    Instantiate(pipePrefab, pipeSpawnPoints[i].position, Quaternion.identity);
            }
        }
    }

    // Spawn the lights at the corresponding spawn points
    private void SpawnLights()
    {
        for (int i = 0; i < lightSpawnPoints.Length; i++)
        {
            if (i < lightPrefabs.Length)
            {
                Instantiate(lightPrefabs[i], lightSpawnPoints[i].position, Quaternion.identity);
            }
        }
    }

    // Get the correct potion prefab based on color
    private GameObject GetPotionPrefabByColor(string color) => color switch
    {
        "Red-Orange" => potionPrefabs[0],   // Red-Orange
        "Yellow-Orange" => potionPrefabs[1], // Yellow-Orange
        "Yellow-Green" => potionPrefabs[2],  // Yellow-Green
        "Blue-Green" => potionPrefabs[3],    // Blue-Green
        "Blue-Purple" => potionPrefabs[4],   // Blue-Purple
        "Red-Purple" => potionPrefabs[5],    // Red-Purple
        _ => null
    };

    // Get the correct pipe prefab based on color
    private GameObject GetPipePrefabByColor(string color) => color switch
    {
        "Red-Orange" => pipePrefabs[0],   // Red-Orange
        "Yellow-Orange" => pipePrefabs[1], // Yellow-Orange
        "Yellow-Green" => pipePrefabs[2],  // Yellow-Green
        "Blue-Green" => pipePrefabs[3],    // Blue-Green
        "Blue-Purple" => pipePrefabs[4],   // Blue-Purple
        "Red-Purple" => pipePrefabs[5],    // Red-Purple
        _ => null
    };


    // Method to handle smashing a potion (e.g., player clicks on it)
    public void SmashPotion(string potionColor)
    {
        // Only proceed if the potion hasn't been smashed yet
        if (unsmashedPotions.Contains(potionColor))
        {
            unsmashedPotions.Remove(potionColor);
            smashedPotions.Add(potionColor);

            // If the potion is part of the correct combination, add it to passedColors
            if (GetCorrectPotionCombinations(targetColor).Contains(potionColor))
            {
                passedColors.Add(potionColor);
            }
        }
    }

    // End the game and show the appropriate result
    private void EndGame()
    {
        List<string> correctColors = GetCorrectPotionCombinations(targetColor);
        int correctLeft = unsmashedPotions.Count(potion => correctColors.Contains(potion));
        int incorrectLeft = unsmashedPotions.Count(potion => otherColors.Except(correctColors).Contains(potion));

        // Determine the result based on the given conditions
        if (correctLeft == 2 && incorrectLeft == 0) {
            resultText.text = "100%!";
        } else if (correctLeft == 1 && incorrectLeft == 2 || correctLeft == 2 && incorrectLeft == 3) {
            resultText.text = "50%!";
        } else if (correctLeft == 0 && incorrectLeft == 0 || correctLeft == 0 && incorrectLeft == 4)
        {
            resultText.text = "zero!";
        } else {
            resultText.text = "0%!";
        }

        if (resultText.text == "100%!") {
            Invoke(nameof(DisplayWinUI), 11f);
        } else if (resultText.text == "zero!") {
            Invoke(nameof(DisplayLoseUI), 1f);
        } else if (resultText.text == "50%!") {
            Invoke(nameof(DisplayLoseUIHalf), 11f);
        } else if (resultText.text == "0%!") {
            Invoke(nameof(DisplayLoseUI), 11f);
        }
    }
    private void DisplayWinUI()
    {
        if (!hasPlayedWinSound)  // Check if the win sound has already been played
        {
            AudioManager.Instance.PlaySFX("win");
            hasPlayedWinSound = true;  // Set flag to true after sound is played
        }

        // Show the Win UI elements
        WinBG.gameObject.SetActive(true);
        WintrophyAnim.gameObject.SetActive(true);
        WinglowAnim.gameObject.SetActive(true);
        nextLvlButton.gameObject.SetActive(true);

        // Trigger animations if they have an Animator component
        if (WintrophyAnim.GetComponent<Animator>() != null)
        {
            WintrophyAnim.GetComponent<Animator>().SetTrigger("PlayAnimation");
        }
        if (WinglowAnim.GetComponent<Animator>() != null)
        {
            WinglowAnim.GetComponent<Animator>().SetTrigger("PlayAnimation");
        }
    }

    private void DisplayLoseUI()
    {
        if (!hasPlayedLoseSound)  // Check if the lose sound has already been played
        {
            AudioManager.Instance.PlaySFX("laughing");
            hasPlayedLoseSound = true;  // Set flag to true after sound is played
        }

        // Show the Lose UI elements
        LoseBG.gameObject.SetActive(true);
        LosetrophyAnim.gameObject.SetActive(true);
        tryAgainBtn.gameObject.SetActive(true);
        LosemenuButton.gameObject.SetActive(true);

        // Trigger animations if they have an Animator component
        if (LosetrophyAnim.GetComponent<Animator>() != null)
        {
            LosetrophyAnim.GetComponent<Animator>().SetTrigger("PlayAnimation");
        }
    }

    private void DisplayLoseUIHalf()
    {
        if (!hasPlayedLoseHalfSound)  // Check if the half lose sound has already been played
        {
            AudioManager.Instance.PlaySFX("failed");
            hasPlayedLoseHalfSound = true;  // Set flag to true after sound is played
        }

        // Show the Lose UI elements
        LoseBG.gameObject.SetActive(true);
        LosetrophyAnimHALF.gameObject.SetActive(true);
        tryAgainBtn.gameObject.SetActive(true);
        LosemenuButton.gameObject.SetActive(true);

        // Trigger animations if they have an Animator component
        if (LosetrophyAnim.GetComponent<Animator>() != null)
        {
            LosetrophyAnim.GetComponent<Animator>().SetTrigger("PlayAnimation");
        }
    }


    // Get the correct potion combinations for the target color
    public List<string> GetCorrectPotionCombinations(string targetColor)
    {
        if (colorCombinations.ContainsKey(targetColor))
        {
            return colorCombinations[targetColor].SelectMany(x => x).ToList();
        }
        return new List<string>();
    }

    public void PickAColorWpassLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
        }

        Debug.Log("LEVEL " + PlayerPrefs.GetInt("levelsUnlocked") + " UNLOCKED");
        SceneManager.LoadScene("PickAColor");
    }
    
}
