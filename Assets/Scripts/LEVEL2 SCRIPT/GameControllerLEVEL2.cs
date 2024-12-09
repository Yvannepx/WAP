using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameControllerLEVEL2 : MonoBehaviour
{
    public MyMovementTouchpad touchpadController;

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
        { "Pistachio", new List<string[]> { new string[] { "Orange", "Green" } } },
        { "Crail", new List<string[]> { new string[] { "Orange", "Purple" } } },
        { "Goblin", new List<string[]> { new string[] { "Green", "Purple" } } }
    };

    // List of other basic potion colors
    private List<string> otherColors = new List<string> { "Orange", "Green", "Purple" };

    // Tracking the potions that have been smashed and those still remaining
    public List<string> smashedPotions = new List<string>();
    public List<string> unsmashedPotions = new List<string>();

    // Target color for the current round
    public string targetColor;
    public HashSet<string> passedColors = new HashSet<string>(); // Track passed target colors

    private void Start()
    {
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

        // End the game if time runs out
        if (gameTime <= 0)
        {
            gameTime = 0;
            EndGame();
            DisableTouchpad();
        }

        // Update the timer text
        gameText.text = Mathf.CeilToInt(gameTime).ToString();
    }

    private void DisableTouchpad()
    {
        // Disable the touchpad controls once the game ends
        if (touchpadController != null)
        {
            touchpadController.DeactivateTouchpad();
        }
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
            "Pistachio" => targetColorImages[0],
            "Crail" => targetColorImages[1],
            "Goblin" => targetColorImages[2],
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

            if (potionInstance.TryGetComponent(out PotionBehaviorLEVEL2 potionBehavior))
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
        "Orange" => potionPrefabs[0],
        "Green" => potionPrefabs[1],
        "Purple" => potionPrefabs[2],
        _ => null
    };

    // Get the correct pipe prefab based on color
    private GameObject GetPipePrefabByColor(string color) => color switch
    {
        "Orange" => pipePrefabs[0],
        "Green" => pipePrefabs[1],
        "Purple" => pipePrefabs[2],
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

        if (correctLeft == 2 && incorrectLeft == 0)
        {
            resultText.text = "Success Rate: 100%!";
        }
        else if (correctLeft == 1 && incorrectLeft == 0 || correctLeft == 1 && incorrectLeft == 1)
        {
            resultText.text = "Success Rate: 50%!";
        }
        else if (correctLeft == 0 || (correctLeft == 1 && incorrectLeft == 2))
        {
            resultText.text = "Success Rate: 0%!";
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
}
