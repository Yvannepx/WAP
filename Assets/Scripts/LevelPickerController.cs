using UnityEngine;

public class LevelPickerController : MonoBehaviour
{
    public GameObject[] levelButtons; // Array of Level buttons in the Level Picker Scene

    public void UnlockLevel(int levelToUnlock)
    {
        // Assuming each level button is indexed and starts locked
        if (levelToUnlock > 0 && levelToUnlock <= levelButtons.Length)
        {
            levelButtons[levelToUnlock - 1].SetActive(true); // Unlock the level button
        }
    }
}
