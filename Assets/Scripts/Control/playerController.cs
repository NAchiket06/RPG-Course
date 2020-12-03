using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class playerController : MonoBehaviour
    {
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {
            if (health.IsDead())
            {
                return;
            }
            if(InterractWithcombat())  return;
            if(InterractWithMovement()) return;
        }

        private bool InterractWithcombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target  = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!GetComponent<fighter>().canAttack(target.gameObject))
                {
                    continue;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InterractWithMovement()
        { 
            Ray ray = GetMouseRay();
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0)) {
                    GetComponent<mover>().StartMoveAction(hit.point,1f);
                }
                return true;

            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}