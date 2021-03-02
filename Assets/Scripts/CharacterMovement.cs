using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //speed the character is moving with
    public float movementSpeed = 1f;

    //the renderer that will display the animation
    CharacterRenderer isoRenderer;

    Rigidbody2D rbody;

    //On starting the game (function is called before Start()) get the necessary components from the character game object, the script is attached to
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<CharacterRenderer>();
    }


    // frame-independent function that uses the frequency of the physics system
    void FixedUpdate()
    {
        //get the current position of the character and the input from 'WASD'
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //save the input into a vector and clamp it to prevent diagonal movement becoming faster than movement in the cardinal directions
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        
        //multiply with movement speed and calculate the new position
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime; //use Time.fixedDeltaTime to make sure the movement is stable across different frame rates

        //set direction for the renderer so it can figure out, what animation to play
        isoRenderer.SetDirection(movement);

        //move the character
        rbody.MovePosition(newPos);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
