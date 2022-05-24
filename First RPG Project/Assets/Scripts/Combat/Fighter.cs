using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;

        Transform combatTarget;

        private void Update()
        {
            if (combatTarget == null) return;
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(combatTarget.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, combatTarget.position) < weaponRange;
        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            combatTarget = target.transform;
        }

        public void Cancel()
        {
            combatTarget = null;
        }
    }
}
