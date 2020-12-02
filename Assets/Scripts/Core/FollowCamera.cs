using RPG.Movement;
using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        public  Transform target;
        public Vector3 offset;

        void Start()
        {
            target = FindObjectOfType<playerController>().gameObject.transform;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = target.position - offset;
        }
    }

}