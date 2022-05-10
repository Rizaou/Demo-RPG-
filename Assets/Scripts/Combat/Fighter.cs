using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] private float weaponRange = 3f;
        [SerializeField] private float timeBetweenAttacks = 2f;
        [SerializeField] private float weaponDamage = 5f;
        Health target;
        float timeSinceLastAttack = 2f;


        private void Update()
        {

            timeSinceLastAttack += Time.deltaTime;

            if (target == null) { return; }

            if (target.IsDead) { return; }

            if (!GetComponent<NavMeshAgent>()) { return; }



            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }


        }

        private void AttackBehaviour()
        {


            transform.LookAt(target.transform);

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject target)
        {
            if (target == null) { return false; }

            Health targetToTest = target.GetComponent<Health>();

            return targetToTest != null && !targetToTest.IsDead;
        }

        public void Attack(GameObject target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = target.GetComponent<Health>();

        }



        //* Animation Event
        public void Hit()
        {

            if (target == null) { return; }
            target?.TakeDamage(weaponDamage);

        }


        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }

}