using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    // Singleton instance
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip drainSound;
    public AudioClip spinningSound;
    public AudioClip lightsSound;
    public AudioClip leverSound;
    public AudioClip loadingSound;
    public AudioClip potionBrewSound;   // Added PotionBrew Sound
    public AudioClip machineErrorSound; // Added MachineError Sound
    public AudioClip winSound;          // Added Win Sound
    public AudioClip loseHALFSound;         // Added Lose Sound
    public AudioClip loseWHOLESound;         // Added Lose Sound
    public AudioClip buttonClickSound;
    public AudioClip smashSound;  // Sound effect for smashing
    
    public Image soundOnIcon;
    public Image soundOffIcon;

    
    public Image musicOnIcon;
    public Image musicOffIcon;
    

    private bool sfxMuted = false; // Mute for SFX
    private bool musicMuted = false; // Mute for Music

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject); 
        }

        if (!PlayerPrefs.HasKey("sfxMuted"))
        {
            PlayerPrefs.SetInt("sfxMuted", 0);
            PlayerPrefs.SetInt("musicMuted", 0);
            Load();
        }
        Load();
        LoadSFX();
        UpdateButtonIcons();
    }

    private void OnEnable()
    {
        Load();        // Load music mute state
        LoadSFX();     // Load SFX mute state

        // Apply the loaded states to the audio sources
        musicSource.mute = musicMuted;
        sfxSource.mute = sfxMuted;

        // Update the button icons to reflect the current mute states
        UpdateButtonIcons();
        UpdateSFXButtonIcon();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            PlayBackgroundMusic();  // Play background music when the game starts
        }
        musicOffIcon.enabled = false;
        soundOffIcon.enabled = false;
    }

    // Play background music
    public void PlayBackgroundMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;  // Loop the music
            musicSource.Play();
        }
    }


    // Stop background music
    public void StopBackgroundMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    // Resume background music
    public void ResumeBackgroundMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }

    // Play a specific sound effect once
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);  // Play the sound effect once (no looping)
        }
    }

    // Convenience methods for specific sound effects
    public void PlayDrainSound()
    {
        PlaySFX(drainSound);
    }

    public void PlaySpinningSound()
    {
        PlaySFX(spinningSound);
    }

    public void PlayLightsSound()
    {
        PlaySFX(lightsSound);
    }

    public void PlayLeverSound()
    {
        PlaySFX(leverSound);
    }

    public void PlayLoadingSound()
    {
        PlaySFX(loadingSound);
    }

    // New methods for PotionBrew and MachineError sounds
    public void PlayPotionBrewSound()
    {
        PlaySFX(potionBrewSound);
    }

    public void PlayMachineErrorSound()
    {
        PlaySFX(machineErrorSound);
    }

    // New methods for Win and Lose UI sounds
    public void PlayWinSound()
    {
        PlaySFX(winSound);
    }

    public void PlayloseHALFSound()
    {
        PlaySFX(loseHALFSound);
    }

    public void PlayloseWHOLESound()
    {
        PlaySFX(loseWHOLESound);
    }
    // Method to play button click sound
    public void PlayButtonClickSound()
    {
        PlaySFX(buttonClickSound);
    }


    //SOUND MANAGER
    public void ToggleMusicMute()
    {
        musicMuted = !musicMuted;
        musicSource.mute = musicMuted; // Mute only the music source
        Save();
        UpdateButtonIcons();

        if (musicMuted)
        {
            StopBackgroundMusic(); // Stop music when muted
        }
        else
        {
            PlayBackgroundMusic(); // Resume music
        }
    }


    // Mute/unmute SFX
    public void ToggleSFXMute()
    {
        if (sfxMuted == false)
        {
            sfxMuted = true;
            sfxSource.mute = true;  // Mute SFX
        }
        else
        {
            sfxMuted = false;
            sfxSource.mute = false; // Unmute SFX
        }
        SaveSFX();
        UpdateSFXButtonIcon();
    }

    private void UpdateButtonIcons()
    {
        if (musicMuted)
        {
            musicOnIcon.enabled = false;
            musicOffIcon.enabled = true;
        }
        else
        {
            musicOnIcon.enabled = true;
            musicOffIcon.enabled = false;
        }
        UpdateSFXButtonIcon();
    }
    
    private void Load()
    {
        musicMuted = PlayerPrefs.GetInt("musicMuted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("musicMuted", musicMuted ? 1 : 0);
    }

    private void SaveSFX()
    {
        PlayerPrefs.SetInt("sfxMuted", sfxMuted ? 1 : 0); // Save SFX mute state
    }

    private void LoadSFX()
    {
        sfxMuted = PlayerPrefs.GetInt("sfxMuted") == 1; // Load SFX mute state
    }

    private void UpdateSFXButtonIcon()
    {
        if (sfxMuted == false)
        {
            soundOnIcon.enabled = true; // Show sound on icon
            soundOffIcon.enabled = false; // Hide sound off icon
        }
        else
        {
            soundOnIcon.enabled = false; // Hide sound on icon
            soundOffIcon.enabled = true; // Show sound off icon
        }
    }
    
    // Play the smash sound effect
    public void PlaySmashSound()
    {
        if (sfxSource != null && smashSound != null)
        {
            sfxSource.PlayOneShot(smashSound);  // Play the sound effect once
        }
    }

    
}
