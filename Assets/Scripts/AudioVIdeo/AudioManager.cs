using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public Image musicButtonImage, sfxButtonImage; // Reference to the button images to change transparency

    private void Awake()
    {
        // Ensure there is only one instance of AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        PlayMusic("Main Menu");
        // Subscribe to scene loading event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Ensure to unsubscribe from event to avoid memory leaks
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (scene.name == "WAPMainMenu" || scene.name == "PickAColor") // Check if we are back in the main menu
    {
        PlayMusic("Main Menu"); // Play the main menu background music
    }

    // Check if we are in the Levels scene
    if (scene.name == "PickAColor") // Replace with your actual scene name
    {
        // Try finding the buttons in the Levels scene
        GameObject musicButton = GameObject.Find("musicbtn");
        GameObject sfxButton = GameObject.Find("sfxbtn");

        if (musicButton != null && sfxButton != null)
        {
            // Successfully found the buttons, now assign the Image components
            musicButtonImage = musicButton.GetComponent<Image>();
            sfxButtonImage = sfxButton.GetComponent<Image>();

            // Debugging to check if the assignment works
            Debug.Log("MusicButton and SFXButton assigned successfully.");
            
            // Now update the button states
            UpdateButtonStates();
        }
    }
}


    public void PlayMusic(string name)
    {
        // Find the sound object in the array using the name
        Sound s = Array.Find(musicSounds, x => x.name == name);

        // Check if the sound was found
        if (s == null)
        {
            Debug.Log("Music Not Found");
        }
        else
        {
            // Assign the clip and play the music
            musicSource.clip = s.audioClip; // Access audioClip property from the Sound class
            musicSource.loop = true; // Set the music to loop
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        // Find the sound object in the SFX array by name
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        // Check if the sound was found
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            // Play the sound effect using PlayOneShot
            sfxSource.PlayOneShot(s.audioClip);
        }
    }

    // Toggles the music on or off
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        
        // Change button color and transparency based on mute state
        if (musicSource.mute)
        {
            // Set the button color to a muted state (e.g., grey) with transparency 100
            musicButtonImage.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            // Reset button color to normal (e.g., original color)
            musicButtonImage.color = new Color(1f, 1f, 1f, 1f);  // White color, full opacity
        }
    }

    // Toggles the sound effects (SFX) on or off
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;

        // Change button color and transparency based on mute state
        if (sfxSource.mute)
        {
            // Set the button color to a muted state (e.g., grey) with transparency 100
            sfxButtonImage.color = new Color(1f, 1f, 1f, 0.5f);  // Grey color, full opacity
        }
        else
        {
            // Reset button color to normal (e.g., original color)
            sfxButtonImage.color = new Color(1f, 1f, 1f, 1f);  // White color, full opacity
        }
    }

    public void MusicVolume(float volume)
    {
        // Adjusts the music volume
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        // Adjusts the SFX volume
        sfxSource.volume = volume;
    }

    public void OnButtonClick()
    {
        AudioManager.Instance.PlaySFX("buttonclick");
    }

    // Update button states based on the current mute state of music and SFX
    private void UpdateButtonStates()
    {
        // Update the music button image based on the mute state
        if (musicSource.mute)
        {
            musicButtonImage.color = new Color(1f, 1f, 1f, 0.5f);  // Grey (muted)
        }
        else
        {
            musicButtonImage.color = new Color(1f, 1f, 1f, 1f);  // Normal (unmuted)
        }

        // Update the SFX button image based on the mute state
        if (sfxSource.mute)
        {
            sfxButtonImage.color = new Color(1f, 1f, 1f, 0.5f);  // Grey (muted)
        }
        else
        {
            sfxButtonImage.color = new Color(1f, 1f, 1f, 1f);  // Normal (unmuted)
        }
    }

    public void SetMusicButtonImage(Image buttonImage)
    {
        musicButtonImage = buttonImage;
    }

    public void SetSFXButtonImage(Image buttonImage)
    {
        sfxButtonImage = buttonImage;
    }

}
