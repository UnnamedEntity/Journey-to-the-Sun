using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightScript : MonoBehaviour
{
    SkullProjectileBehaviour SkullProjectileBehaviour;

    private void Start()
    {
        SkullProjectileBehaviour = transform.parent.GetComponent<SkullProjectileBehaviour>();
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.right.normalized * SkullProjectileBehaviour.speed * Time.deltaTime;
    }
}
