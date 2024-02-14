using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownScript : MonoBehaviour
{
    SkullProjectileBehaviour SkullProjectileBehaviour;

    private void Start()
    {
        SkullProjectileBehaviour = transform.parent.GetComponent<SkullProjectileBehaviour>();
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.down.normalized * SkullProjectileBehaviour.speed * Time.deltaTime;
    }
}
