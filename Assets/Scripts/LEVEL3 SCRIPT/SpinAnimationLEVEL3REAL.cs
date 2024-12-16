    using UnityEngine;
    using System.Collections; // Add this to enable IEnumerator

    public class SpinAnimationLEVEL3 : MonoBehaviour
    {
        private Animator mAnimator;
        private GameControllerLEVEL3REAL gameController;
        private bool hasTriggered = false; // To ensure the animation is triggered only once

        void Start()
        {
            // Get the Animator component attached to this GameObject
            mAnimator = GetComponent<Animator>();

            // Find the first GameControllerLEVEL3REAL in the scene
            gameController = Object.FindFirstObjectByType<GameControllerLEVEL3REAL>();

            if (gameController == null)
            {
                Debug.LogError("GameControllerLEVEL3REAL not found in the scene!");
            }
        }

        void Update()
        {
            if (mAnimator != null && gameController != null)
            {
                // Get the color of the current tunnel from the prefab name
                string prefabName = gameObject.name.ToLower(); // Get the name of the GameObject and convert it to lowercase for consistency
                string potionColor = GetPotionColorFromName(prefabName);

                // Debugging: Print the list of smashed potions and the current color
                // Debug.Log("Smashed Potions: " + string.Join(", ", gameController.smashedPotions));
                // Debug.Log("Current Potion Color: " + potionColor);

                // Only trigger animation if the color is NOT in the smashedPotions list
                if (!gameController.smashedPotions.Contains(potionColor) && gameController.gameTime <= 0 && !hasTriggered)
                {
                    mAnimator.SetTrigger("TrDraining");
                    // AudioManager.Instance.PlaySFX("drain");
                    mAnimator.SetTrigger("TrSpinning");
                    AudioManager.Instance.PlaySFX("spinning");
                    StartCoroutine(TriggerLightWithDelay());  // Start the coroutine for delayed TrLight trigger
                    
                    StartCoroutine(TriggerLeverWithDelay());  // Start the coroutine for delayed TrLever trigger
                    hasTriggered = true; // Mark as triggered to prevent multiple activations
                    StartCoroutine(TriggerLoadingWithDelay()); // Start the coroutine for delayed TrLoading trigger
                }
            }

            // Debugging: Check if the spin animation is currently playing
            if (mAnimator != null)
            {
                AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsName("SpinStateName")) // Replace with the actual animation state name
                {
                    Debug.Log("Spin animation is playing.");
                }
            }
        }

        // Coroutine to trigger TrLight after a 3-second delay
        private IEnumerator TriggerLightWithDelay()
        {
            yield return new WaitForSeconds(3f); // Wait for 3 seconds
            AudioManager.Instance.PlaySFX("lights");
            mAnimator.SetTrigger("TrLight");
            Debug.Log("TrLight triggered after 3-second delay.");
        }
        
        // Coroutine to trigger TrLever after a 3.5-second delay
        private IEnumerator TriggerLeverWithDelay()
        {
            yield return new WaitForSeconds(3.5f); // Wait for 3.5 seconds
            AudioManager.Instance.PlaySFX("lever");
            mAnimator.SetTrigger("TrLever");
            Debug.Log("TrLever triggered after 3.5-second delay.");
        }

        // Coroutine to trigger TrLoading after a 4-second delay
        private IEnumerator TriggerLoadingWithDelay()
        {
            yield return new WaitForSeconds(4.5f); // Wait for 4 seconds
            AudioManager.Instance.PlaySFX("loading");
            mAnimator.SetTrigger("TrLoading");
            Debug.Log("TrLoading triggered after 4.5-second delay.");
        }

        // Helper method to extract the potion color from the prefab name
        private string GetPotionColorFromName(string prefabName)
        {
            // Check for the color in the prefab name and return the corresponding new color
            if (prefabName.Contains("red-orange"))
                return "Red-Orange";
            else if (prefabName.Contains("yellow-orange"))
                return "Yellow-Orange";
            else if (prefabName.Contains("yellow-green"))
                return "Yellow-Green";
            else if (prefabName.Contains("blue-green"))
                return "Blue-Green";
            else if (prefabName.Contains("blue-purple"))
                return "Blue-Purple";
            else if (prefabName.Contains("red-purple"))
                return "Red-Purple";
            else
                return string.Empty; // Return empty string if no color match is found
        }

    }
