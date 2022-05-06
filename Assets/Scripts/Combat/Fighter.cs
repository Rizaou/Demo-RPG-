using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] private float weaponRange = 3f;
        [SerializeField] private float timeBetweenAttacks = 2f;
        [SerializeField] private float weaponDamage = 5f;
        Transform target;
        float timeSinceLastAttack = 0f;

        private void Update()
        {

            timeSinceLastAttack += Time.deltaTime;

            if (target == null) { return; }

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }


        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }

        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = target.transform;

        }



        //* Animation Event
        public void Hit()
        {
            if (target.TryGetComponent<Health>(out Health targetHealth))
            {
                targetHealth.TakeDamage(weaponDamage);
            }
        }


        public void Cancel()
        {
            target = null;
            GetComponent<Animator>().SetBool("attack", false);
        }
    }

}