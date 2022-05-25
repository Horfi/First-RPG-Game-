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

        Transform combatTarget;
        float timeScienceLastAtack = 0;

        private void Update()
        {
            timeScienceLastAtack += Time.deltaTime;
            if (combatTarget == null) return;
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(combatTarget.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                Attackbehaviour();
            }
        }

        private void Attackbehaviour()
        {
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
            Health healthComponent = combatTarget.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
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
            GetComponent<Animator>().SetTrigger("stopAttack");
            combatTarget = null;
        }

    }
}
