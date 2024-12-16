using UnityEngine;

public class AspectRatioEnforcer : MonoBehaviour
{
    void Start()
    {
        // Set target aspect ratio to 1024:1280
        float targetAspect = 1024f / 1280f;

        // Get current screen's aspect ratio
        float windowAspect = (float)Screen.width / Screen.height;

        // Calculate the scale for the viewport
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            // Add letterbox (top and bottom padding)
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            // Add pillarbox (left and right padding)
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
