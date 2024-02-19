using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownScript : MonoBehaviour
{
    SkullProjectileBehaviour _SkullProjectileBehaviour;

    private void Start()
    {
        _SkullProjectileBehaviour = transform.parent.GetComponent<SkullProjectileBehaviour>();
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.down.normalized * _SkullProjectileBehaviour.speed * Time.deltaTime;
    }
}
