using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnterCollider : MonoBehaviour
{
    public GameObject DoorController;
    DoorController doorControllerScript;

    private void Start()
    {
        DoorController = GameObject.Find("DoorController");
        doorControllerScript = DoorController.GetComponent<DoorController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            doorControllerScript.defeatedEnemies = false;
            doorControllerScript.EnableDisableDoors();
        }
        
    }
}
