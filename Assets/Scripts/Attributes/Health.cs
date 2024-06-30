using System;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 60;
        float healthPoints = -1f;
        bool isDead = false;

        private void Start()
        {
            if(healthPoints < 0) healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void RegenerateHealth()
        {
            float regenHP = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints = Mathf.Max(healthPoints, regenHP);
        }

        public bool IsDead(){
            return isDead;
        }



        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
            if(healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHP() {
            return healthPoints;
        }

        public float GetMaxHP()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if(experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperiencePoint)); 
        }

        public float GetPercentage() {
            return healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health) * 100;
        }

        private void Die()
        {
            if(isDead) return; //이미 죽어있다면 EarlyReturn

            isDead = true;
            GetComponent<Animator>().SetTrigger("die"); //사망 애니메이션 출력
            GetComponent<ActionScheduler>().CancelCurrentAction(); //사망시 행동 캔슬
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if(healthPoints == 0)
            {
                Die();
            }
        }
    }
}