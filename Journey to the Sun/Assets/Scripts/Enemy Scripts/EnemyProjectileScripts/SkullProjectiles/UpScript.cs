using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpScript : MonoBehaviour
{
    SkullProjectileBehaviour SkullProjectileBehaviour;

    private void Start()
    {
        SkullProjectileBehaviour = transform.parent.GetComponent<SkullProjectileBehaviour>();
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.up.normalized * SkullProjectileBehaviour.speed * Time.deltaTime;
    }
}
