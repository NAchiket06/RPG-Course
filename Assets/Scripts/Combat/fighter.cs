using RPG.Movement;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class fighter : MonoBehaviour,IAction
    {
        Health target;

        [SerializeField] float weaponRange = 2f,handDamage = 30f;
        float timeSinceLastAttack = 0;
        public float timeBetweenAttacks  =1f;
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
            {
                GetComponent<Animator>().SetTrigger("stopAttack");
                return;
            }
            if (target.IsDead()) return;
            if (!GetIsInRange()) // not in range of target
            {
                GetComponent<mover>().MoveTo(target.transform.position);
            }
            else // target in range
            {
                
                GetComponent<mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("attack");
            timeSinceLastAttack = 0f;
        }

        //animation Event
        void Hit()
        {
            if (target == null) return;
            target.GetComponent<Health>().TakeDamage(handDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool canAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }


    }

}