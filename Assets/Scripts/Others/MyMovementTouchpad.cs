using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMovementTouchpad : MonoBehaviour
{
    public GameObject targetobject;
    public float mspeed = 0.005f;

    public static MyMovementTouchpad instance;

    [HideInInspector]
    public bool istouchpadactive;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void ActivateTouchpad() //Pointer Down Event-trigger
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    public void DeactivateTouchpad() //Pointer Up Event-trigger
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    private Vector2 difference = Vector2.zero;
    private Vector2 originalPosition; // Store the original position

    private void OnMouseDown()
    {
        // Calculate the difference between the object's position and the mouse position
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        // Move the object while dragging
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    public void OnMouseUp()
    {
        // Reset the object to its original position
        transform.position = originalPosition;
    }

    public void EnableTouchpad()
    {
        originalPosition = transform.position;  // Store the initial position of the object when the game starts
    }
}