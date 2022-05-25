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
                TriggerAtack();
                timeScienceLastAtack = 0;

            }

        }

        private void TriggerAtack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event
        void Hit()
        {
            if(combatTarget == null) return;
            combatTarget.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, combatTarget.transform.position) < weaponRange;
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if(combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            combatTarget = target.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAtack();
            combatTarget = null;
        }

        private void StopAtack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
