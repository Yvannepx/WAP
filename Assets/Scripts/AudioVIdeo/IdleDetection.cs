using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class IdleDetection : MonoBehaviour
{
    [Header("Inactivity Settings")]
    public float idleTime = 60f; // Time to wait before playing the video

    [Header("UI References")]
    public GameObject videoPlayerPanel; // Panel to display the video
    public VideoPlayer videoPlayer; // The VideoPlayer component

    [Header("Audio References")]
    public AudioSource mainMenuMusic; // The AudioSource playing background music

    private float timeSinceLastInteraction; // Tracks idle time
    private bool isVideoPlaying = false;

    private void Start()
    {
        timeSinceLastInteraction = 0f;
        videoPlayerPanel.SetActive(false); // Ensure the video panel is hidden initially

        // Ensure audio output is set to AudioSource
        if (videoPlayer.audioOutputMode != VideoAudioOutputMode.AudioSource)
        {
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            AudioSource audioSource = videoPlayer.gameObject.AddComponent<AudioSource>();
            videoPlayer.SetTargetAudioSource(0, audioSource);
        }
    }

    private void Update()
    {
        // Track idle time
        timeSinceLastInteraction += Time.deltaTime;

        // Check if user is idle for specified time
        if (timeSinceLastInteraction >= idleTime && !isVideoPlaying)
        {
            PlayIdleVideo(); // Play video after idle time
        }

        // Reset idle timer if input is detected
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            ResetIdleTimer();
        }
    }

    private void PlayIdleVideo()
    {
        isVideoPlaying = true;
        videoPlayerPanel.SetActive(true); // Show the video panel
        videoPlayer.Play(); // Play the video

        // Mute the background music
        if (mainMenuMusic != null)
        {
            mainMenuMusic.mute = true;
        }

        Debug.Log(videoPlayer.isPrepared);
    }

    private void ResetIdleTimer()
    {
        timeSinceLastInteraction = 0f; // Reset the timer
        if (isVideoPlaying)
        {
            isVideoPlaying = false;
            videoPlayer.Stop(); // Stop the video if it's playing
            videoPlayerPanel.SetActive(false); // Hide the video panel

            // Unmute the background music
            if (mainMenuMusic != null)
            {
                mainMenuMusic.mute = false;
            }
        }
    }
}
