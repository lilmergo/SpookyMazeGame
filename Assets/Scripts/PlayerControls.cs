using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Vector2 speed = new Vector2(50, 50); //public speed variable

    
    void Update()
    {
        //get wasd input
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");


        //move player based on inputX +inputY
        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        //make the object move through the same distance on different devices.
        movement *= Time.deltaTime;

        //apply movement to the player game object transform
        transform.Translate(movement);


    }
}
