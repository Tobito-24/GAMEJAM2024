using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Ensure the component is present on the gameobject the script is attached to
// Uncomment this if you want to enforce the object to require the RB2D component to be already attached
// [RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 5.0f; // Public variable to control movement speed
    public Rigidbody2D rigidbody2D; // Local rigidbody variable to hold a reference to the attached Rigidbody2D component
    public Camera cam; // Local camera variable to hold a reference to the main camera
    Vector2 movement;
    Vector2 mousePos;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
    }

    void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + (movement * MovementSpeed * Time.fixedDeltaTime));
        Vector2 lookDir = mousePos - rigidbody2D.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rigidbody2D.rotation = angle;
    }
}