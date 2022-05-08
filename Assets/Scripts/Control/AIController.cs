using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspitionTime = 3f;

        private float lastTimeSawPlayer = 0f;

        private Fighter fighter;
        private Mover mover;
        private GameObject player;
        private Health health;
        private Vector3 guardPosition;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardPosition = transform.position;

        }
        private void Update()
        {

            if (health.IsDead) { return; }

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour();
                lastTimeSawPlayer = 0f;
            }
            else if (!InAttackRangeOfPlayer() && lastTimeSawPlayer >= suspitionTime)
            {
                lastTimeSawPlayer += Time.deltaTime;
                SuspiciousBehaviour();
            }
            else
            {
                lastTimeSawPlayer += Time.deltaTime;
                GuardBehaviour();

            }

        }

        private void GuardBehaviour()
        {
            fighter.Cancel();
        }

        private void SuspiciousBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= chaseDistance;
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);

        }
    }
}
