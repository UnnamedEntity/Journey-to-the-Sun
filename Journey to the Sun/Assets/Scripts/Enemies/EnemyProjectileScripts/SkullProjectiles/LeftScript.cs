using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftScript : MonoBehaviour
{
    SkullProjectileBehaviour SkullProjectileBehaviour;

    private void Start()
    {
        SkullProjectileBehaviour = transform.parent.GetComponent<SkullProjectileBehaviour>();   
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.left.normalized * SkullProjectileBehaviour.speed * Time.deltaTime;
    }
}
