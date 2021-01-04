using UnityEngine;
using System.Collections;
using RPG.Saving;

namespace RPG.Core {
    public class Health : MonoBehaviour,ISaveable
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

        public object CaptureState()
        {
            return health;
        }
        public void RestoreState(object state)
        {
            health = (float)state;
            if (health == 0f)
            {
                Die();
            }
        }




    }
}