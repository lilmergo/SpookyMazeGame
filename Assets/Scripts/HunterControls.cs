using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HunterControls : MonoBehaviour
{
    public Vector2 speed = new Vector2(50, 50); //public speed variable
    public List<GameObject> prey = new List<GameObject>();
    public float killRadius;

    private void Start()
    {
        DetectPrey();
    }
    void Update()
    {
        HandleInput();
        KillPrey();
    }


    //find all gameobjects tagged with prey and add to prey list
    void DetectPrey()
    {
        prey.AddRange(GameObject.FindGameObjectsWithTag("Prey"));
    }

    //hunter movement
    void HandleInput()
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

    //on mouse click for all prey detected (stored in prey list) within radius - delete
    void KillPrey()
    {
        foreach (GameObject player in prey.ToList())
        {
            if (Input.GetKey(KeyCode.Mouse0) && prey != null)
            {
                if ((Vector3.Distance(player.transform.position, transform.position) < killRadius) && prey != null)
                {
                    prey.Remove(player);
                    Destroy(player);

                }
            }
        }
    }
}
