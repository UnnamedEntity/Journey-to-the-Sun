using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetBehaviour : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.position = PlayerMovement.transform.position + new Vector3(-1.5f, 0, 0);
        //    canShoot = false;
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.position = PlayerMovement.transform.position + new Vector3(1.5f, 0, 0);
        //    canShoot = false;
        //}
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.position = PlayerMovement.transform.position + new Vector3(0, 1.5f, 0);
        //    canShoot = false;
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.position = PlayerMovement.transform.position + new Vector3(0, -1.5f, 0);
        //    canShoot = false;
        //}
    }
}
