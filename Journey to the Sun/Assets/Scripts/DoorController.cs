using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool defeatedEnemies = false;

    
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            defeatedEnemies = true;
        }
        if (defeatedEnemies == true)
        {
            defeatedEnemies = false;
            EnableDisableDoors();
        }
    }

    public void EnableDisableDoors()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door"); //Creates an array of every gameobject with the tag "Door"

        foreach (GameObject door in doors) //Iterates through the array, applying code to every object in the array
        {
            if (door.GetComponent<Renderer>().enabled) //Checks to see if the renderer is enabled or not
            {
                //Disables both the renderer and 2D collider of the object
                door.GetComponent<Renderer>().enabled = false;
                door.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                door.GetComponent<Renderer>().enabled = true;
                door.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
