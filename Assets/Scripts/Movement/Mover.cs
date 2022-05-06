using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {

        [SerializeField] private Animator _animator;


        private NavMeshAgent _agent;
        private Ray _lastRay;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {

            HandleAnimation();
        }

        private void HandleAnimation()
        {
            float speedVector = transform.InverseTransformDirection(_agent.velocity).magnitude;
            float val = Mathf.Abs(speedVector);
            _animator.SetFloat("forwardSpeed", val);
        }


        public void Stop()
        {

            _agent.isStopped = true;
            _agent.ResetPath();
        }

        public void StartMoveAction(Vector3 point)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(point);
        }
        public void MoveTo(Vector3 point)
        {
            _agent.ResetPath();
            _agent.SetDestination(point);
            _agent.isStopped = false;
        }
    }

}