using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {

        [SerializeField] private Animator _animator;


        private NavMeshAgent _agent;
        private Ray _lastRay;
        private Health health;
        private float maxSpeed = 5f;

        private void Start()
        {
            health = GetComponent<Health>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _agent.enabled = !health.IsDead;
            HandleAnimation();
        }

        private void HandleAnimation()
        {
            float speedVector = transform.InverseTransformDirection(_agent.velocity).magnitude;
            float val = Mathf.Abs(speedVector);
            _animator.SetFloat("forwardSpeed", val);
        }


        public void Cancel()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();

            if (_agent.enabled)
                _agent!.isStopped = true;


        }

        public void StartMoveAction(Vector3 point, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(point, speedFraction);
        }
        public void MoveTo(Vector3 point, float speedFraction)
        {
            if (_agent.enabled == false) { return; }

            _agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            _agent.SetDestination(point);
            _agent.isStopped = false;
        }


    }

}