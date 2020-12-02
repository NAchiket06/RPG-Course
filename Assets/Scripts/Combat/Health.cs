using UnityEngine;
using System.Collections;

namespace RPG.Combat {
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;
        bool isDead = false;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0f);
            if(health == 0f && !isDead)
            {
                isDead = true;
                Die();
            }
        }

        void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
        }

        
    }
}