using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger2dScriptLEVEL3 : MonoBehaviour
{
    public GameControllerLEVEL3REAL gameController;
    public Sprite[] hammerSprites; // Array to hold the hammer sprites (1-stand, 2-slight bend, 3-right bend, 4-right bend with smash effect)
    private SpriteRenderer hammerRenderer;
    private bool isTriggering = false;

    [SerializeField]
    private AudioClip smashSound; // Sound effect for smashing
    private AudioSource audioSource; // Audio source component

    // Explosion effects for each potion color
    [SerializeField]
    private GameObject redOrangeExplosion;   // Explosion effect for Red-Orange
    [SerializeField]
    private GameObject yellowOrangeExplosion; // Explosion effect for Yellow-Orange
    [SerializeField]
    private GameObject yellowGreenExplosion; // Explosion effect for Yellow-Green
    [SerializeField]
    private GameObject blueGreenExplosion;   // Explosion effect for Blue-Green
    [SerializeField]
    private GameObject bluePurpleExplosion;  // Explosion effect for Blue-Purple
    [SerializeField]
    private GameObject redPurpleExplosion;   // Explosion effect for Red-Purple


    // Reference to the cracked bottle prefab
    [SerializeField]
    private GameObject crackedBottlePrefab;

    private Dictionary<string, GameObject> explosionEffects;

    void Start()
    {
        // Get the SpriteRenderer component from the hammer object (you can set this reference in the Inspector too)
        hammerRenderer = GetComponent<SpriteRenderer>();

        // Add or get the AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();

        // Initialize the dictionary mapping colors to explosions
        explosionEffects = new Dictionary<string, GameObject>
        {
            { "Red-Orange", redOrangeExplosion },    // Red-Orange explosion effect
            { "Yellow-Orange", yellowOrangeExplosion }, // Yellow-Orange explosion effect
            { "Yellow-Green", yellowGreenExplosion }, // Yellow-Green explosion effect
            { "Blue-Green", blueGreenExplosion },    // Blue-Green explosion effect
            { "Blue-Purple", bluePurpleExplosion },  // Blue-Purple explosion effect
            { "Red-Purple", redPurpleExplosion },    // Red-Purple explosion effect
        };

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the PotionBehaviorLEVEL3REAL component from the collided object
        PotionBehaviorLEVEL3REAL potionBehavior = collision.GetComponent<PotionBehaviorLEVEL3REAL>();

        if (potionBehavior != null)
        {
            // Get the potion's color
            string potionColor = potionBehavior.potionColor;

            // Check if there's a corresponding explosion effect
            if (explosionEffects.ContainsKey(potionColor))
            {
                // Instantiate the appropriate explosion effect at the potion's position
                Instantiate(explosionEffects[potionColor], potionBehavior.transform.position, Quaternion.identity);
                Debug.Log($"Potion {potionColor} triggered!");
            }
            else
            {
                Debug.LogWarning($"No explosion effect found for potion color: {potionColor}");
            }

            // Spawn the cracked bottle at the same position as the original potion
            if (crackedBottlePrefab != null)
            {
                Instantiate(crackedBottlePrefab, potionBehavior.transform.position, potionBehavior.transform.rotation);
            }

            // Destroy the potion and play the smash animation
            potionBehavior.DestroyInstantly();

            // Play the smash sound
            AudioManager.Instance.PlaySFX("potionbreak");

            // Start the hammer animation to simulate the smashing effect (1 -> 4)
            isTriggering = true;
            StartCoroutine(AnimateHammer(true));
        }
    }

    void OnTriggerExit2D()
    {
        // When exiting the trigger, stop triggering and animate the hammer back (4 -> 1)
        Debug.Log("Trigger exited");

        isTriggering = false;
        StartCoroutine(AnimateHammer(false));
    }

    // Coroutine to animate the hammer
    private IEnumerator AnimateHammer(bool isSmashing)
    {
        if (isSmashing)
        {
            // Animate from sprite 1 to 4
            for (int i = 1; i < hammerSprites.Length; i++)
            {
                hammerRenderer.sprite = hammerSprites[i];
                yield return new WaitForSeconds(0.05f); // Adjust time between frames
            }
        }
        else
        {
            // Animate from sprite 4 to 1
            for (int i = hammerSprites.Length - 1; i >= 0; i--)
            {
                hammerRenderer.sprite = hammerSprites[i];
                yield return new WaitForSeconds(0.05f); // Adjust time between frames
            }
        }
    }

    // Method to play the smash sound
    private void PlaySmashSound()
    {
        if (audioSource != null && smashSound != null)
        {
            audioSource.PlayOneShot(smashSound);
        }
    }
}
