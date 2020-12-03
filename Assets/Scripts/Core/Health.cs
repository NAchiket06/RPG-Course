using UnityEngine;
using System.Collections;

namespace RPG.Core {
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0f);
            if(health == 0f)
            {
                Die();
            }
        }

        void Die()
        {
            if (IsDead()) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().cancelCurrentAction();
        }

        
    }
}