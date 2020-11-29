using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Transform target;
    public Vector3 offset;

    void Start()
    {
        target = FindObjectOfType<mover>().gameObject.transform ;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position - offset;
    }
}
