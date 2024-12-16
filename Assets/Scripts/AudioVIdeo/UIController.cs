using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // public Slider _musicSlider, _sfxSlider;

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    //FOR SETTINGS
    
    // References to the images you want to show/hide
    public Image settingsImage;
    public Image potionCollectionImage;
    public Image howToPlayImage;

    // Method to show the Settings Image
    public void ShowSettings()
    {
        settingsImage.gameObject.SetActive(true);
        potionCollectionImage.gameObject.SetActive(false);  // Hide other images
        howToPlayImage.gameObject.SetActive(false);
    }

    // Method to show the Potion Collection Image
    public void ShowPotionCollection()
    {
        potionCollectionImage.gameObject.SetActive(true);
        settingsImage.gameObject.SetActive(false);  // Hide other images
        howToPlayImage.gameObject.SetActive(false);
    }

    // Method to show the How to Play Image
    public void ShowHowToPlay()
    {
        howToPlayImage.gameObject.SetActive(true);
        settingsImage.gameObject.SetActive(false);  // Hide other images
        potionCollectionImage.gameObject.SetActive(false);
    }

    // Method to hide all images (close all)
    public void CloseAll()
    {
        settingsImage.gameObject.SetActive(false);
        potionCollectionImage.gameObject.SetActive(false);
        howToPlayImage.gameObject.SetActive(false);
    }

    public void ClosepotionCollectionImage()
    {
        potionCollectionImage.gameObject.SetActive(false);
        settingsImage.gameObject.SetActive(true);
    }
    
    public void ClosehowToPlayImage()
    {
        howToPlayImage.gameObject.SetActive(false);
        settingsImage.gameObject.SetActive(true);
    }
    
    // Reference to the button itself, which you can set in the Inspector
    public Button musicButton;
    public Button sfxButton;

    // Add references to the button images
    public Image musicButtonImage;
    public Image sfxButtonImage;

    private void Start()
    {
        // Add listeners to the buttons
        if (musicButton != null)
        {
            musicButton.onClick.AddListener(() => AudioManager.Instance.ToggleMusic());
        }

        if (sfxButton != null)
        {
            sfxButton.onClick.AddListener(() => AudioManager.Instance.ToggleSFX());
        }

        // Assign button images to the AudioManager
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicButtonImage(musicButtonImage);
            AudioManager.Instance.SetSFXButtonImage(sfxButtonImage);
        }
    }
}
