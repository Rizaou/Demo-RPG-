using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;
using RPG.Core;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private Health health;
        private void Start()
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (health.IsDead) { return; }
            if (InteractWithCombat()) { return; }
            if (InteractWithMovement()) { return; }


        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent<CombatTarget>(out CombatTarget target))
                {
                    if (target == null) { continue; }

                    if (!GetComponent<Fighter>().CanAttack(target.gameObject)) { continue; }

                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponent<Fighter>().Attack(target.gameObject);
                    }

                    return true;

                }


            }

            return false;
        }

        private bool InteractWithMovement()
        {

            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    gameObject.GetComponent<Mover>().StartMoveAction(hit.point, 1f);

                }

                return true;

            }

            return false;


        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}