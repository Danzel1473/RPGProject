using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;


        private void Start() {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }


        public bool IsDead(){
            return isDead;
        }



        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
            if(healthPoints == 0)
            {
                Die();
            }
        }

        public float GetPercentage() {
            return healthPoints / GetComponent<BaseStats>().GetHealth() * 100;
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