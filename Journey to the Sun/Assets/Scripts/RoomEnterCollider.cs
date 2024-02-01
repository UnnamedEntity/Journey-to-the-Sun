using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnterCollider : MonoBehaviour
{
    public GameObject DoorController;
    DoorController doorControllerScript;

    private void Start()
    {
        doorControllerScript = DoorController.GetComponent<DoorController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        doorControllerScript.defeatedEnemies = false;
        doorControllerScript.EnableDisableDoors();
    }
}
