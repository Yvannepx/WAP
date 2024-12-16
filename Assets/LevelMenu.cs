using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    int levelsUnlocked;
    public Button[] buttons;
    void Start() 
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for (int i = 0; i < buttons.Length; i++) 
        { 
            buttons[i].interactable = false;
        } 

        for (int i = 0; i < levelsUnlocked; i++) 
        {    
            buttons[i].interactable = true;
        }
        
        // Adjust the opacity of images based on the unlocked levels
        UpdateImagesOpacity();
    } 

    private void UpdateImagesOpacity()
    {
        for (int i = 0; i < images.Length; i++)
        {
            // Set opacity of images: 1 for unlocked levels, 0.5 for others
            Color tempColor = images[i].color;
            if (i < levelsUnlocked)
            {
                tempColor.a = 1f;  // Fully visible for unlocked levels
            }
            else
            {
                tempColor.a = 0.5f; // Semi-transparent for locked levels
            }
            images[i].color = tempColor;
        }
    }
    
    public void Loadlevel(int levelIndex) {
        SceneManager.LoadScene(levelIndex);
    }


    public Image[] images; // Array for 12 images

}
