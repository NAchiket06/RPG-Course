using RPG.Movement;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class fighter : MonoBehaviour,IAction
    {
        Transform target;

        [SerializeField] float weaponRange = 2f;

        void Update()
        {
            if (target == null) return;
            if (target != null && !GetIsInRange())
            {
                GetComponent<mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;

        }

        //animation Event
        void Hit()
        {

        }
    }

}