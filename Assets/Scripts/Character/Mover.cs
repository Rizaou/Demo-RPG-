using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

public class Mover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    // [SerializeField] private Transform _target;

    private Ray _lastRay;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();

        }

        _animator.SetFloat("forwardSpeed",Mathf.Abs(transform.InverseTransformDirection(_agent.velocity).z));

    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            _agent.SetDestination(hit.point);
            _lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }


    }
}
