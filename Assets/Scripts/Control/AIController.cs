using System;
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
        
        [SerializeField] PatrolPath patrolPath;
        float waypointTolerance = 1f, dwellTime = 2f,timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        [SerializeField] Vector3 guardPosition;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.6f;

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

            if (health.IsDead())
            {
                return;
            }
            if (InAttackRange() && fight.CanAttack(player))
            {
                AttackBehviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspiciousBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimes();

        }

        private void UpdateTimes()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void AttackBehviour()
        {
            timeSinceLastSawPlayer = 0f;
            fight.Attack(player);
        }

        private void SuspiciousBehaviour()
        {
            GetComponent<ActionScheduler>().cancelCurrentAction();
        }



        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0f;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if (timeSinceArrivedAtWaypoint > dwellTime)
            {
                GetComponent<mover>().StartMoveAction(nextPosition,patrolSpeedFraction);

            }
        }
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.getNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
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