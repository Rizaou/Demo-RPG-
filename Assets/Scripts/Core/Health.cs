using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] private float healthPoints = 100f;
        private bool isDead = false;
        public bool IsDead => isDead;


        public void TakeDamage(float damage)
        {
            if (isDead) { return; }
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
        }

        public object CaptureState()
        {
            return healthPoints;

        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints == 0 )
            {
                Die();
            }
        }

    }

}