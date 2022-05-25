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
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage;

        Health combatTarget;
        float timeScienceLastAtack = 0;

        private void Update()
        {
            timeScienceLastAtack += Time.deltaTime;
            if (combatTarget == null) return;
            if(combatTarget.IsDead()) return;
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(combatTarget.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                Attackbehaviour();
            }
        }

        private void Attackbehaviour()
        {
            transform.LookAt(combatTarget.transform);
            if (timeScienceLastAtack > timeBetweenAttacks)
            {
                // trigger hit() event
                GetComponent<Animator>().SetTrigger("attack");
                timeScienceLastAtack = 0;

            }

        }
        // Animation Event
        void Hit()
        {
            Health healthComponent = combatTarget;
            combatTarget.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, combatTarget.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            combatTarget = target.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            combatTarget = null;
        }

    }
}
