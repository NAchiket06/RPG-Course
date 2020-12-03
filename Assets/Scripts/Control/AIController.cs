using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        fighter fight;
        GameObject player;
        Health health;

        private void Start()
        {
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
                fight.Attack(player);
            }
            else
            {
                fight.Cancel();
            }
        }

        private bool InAttackRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }
    }

}