using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f,timeSinceLastSawPlayer = Mathf.Infinity,suspicionTime=5f;
        [SerializeField] Vector3 guardPosition;

        fighter fight;
        GameObject player;
        Health health;
        private float gizmoRadius = 5f;

        private void Start()
        {
            guardPosition = transform.position;
            health = GetComponent<Health>();
            fight = GetComponent<fighter>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {

            if(health.IsDead())
            {
                return;
            }
            if (InAttackRange() && fight.canAttack(player) )
            {
                timeSinceLastSawPlayer = 0f;
                AttackBehviour();
            }
            else if(timeSinceLastSawPlayer < suspicionTime)
            {
                SuspiciousBehaviour();
            }
            else
            {
                GuardBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;

        }

        private void AttackBehviour()
        {
            fight.Attack(player);
        }

        private void SuspiciousBehaviour()
        {
            GetComponent<ActionScheduler>().cancelCurrentAction();
        }



        private void GuardBehaviour()
        {
            GetComponent<mover>().MoveTo(guardPosition);
        }

     

        private bool InAttackRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, gizmoRadius);
        }
        
    }

}