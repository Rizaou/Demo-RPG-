using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {

        [SerializeField] private float weaponRange = 3f;
        Transform target;

        private void Update()
        {

            if (target == null) { return; }

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }


        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Fight(CombatTarget target)
        {
            this.target = target.transform;

        }


        public void Cancel()
        {
            target = null;
        }
    }

}