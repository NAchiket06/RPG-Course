using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class fighter : MonoBehaviour
    {
        Transform target;

        [SerializeField] float weaponRange = 2f;


        void Update()
        {
            bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange;
            if (target != null && !isInRange)
            {
                GetComponent<mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<mover>().Stop();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }
    }

}