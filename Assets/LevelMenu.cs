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
    } 
    public void Loadlevel(int levelIndex) {
        SceneManager.LoadScene(levelIndex);
    }
}
