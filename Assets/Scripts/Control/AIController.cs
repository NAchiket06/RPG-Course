﻿using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        fighter fight;
        GameObject player;

        private void Start()
        {
            fight = GetComponent<fighter>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
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