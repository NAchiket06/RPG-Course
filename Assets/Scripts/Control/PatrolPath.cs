using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {

        float sphereRad = 0.3f;
        private void OnDrawGizmos()
        {
            int j;
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.green;
                j = getNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), sphereRad);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public int getNextIndex(int i)
        {
            if(i +1 == transform.childCount )
            {
                return 0;
            }
            return i+1;
        }
    }

}