using System;
using Newtonsoft.Json.Linq;
using RPG.Saving;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable//, IJsonSaveable
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;


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

        private void Die()
        {
            if(isDead) return; //이미 죽어있다면 라면 EarlyReturn

            isDead = true;
            GetComponent<Animator>().SetTrigger("die"); //사망 애니메이션 출력
            GetComponent<ActionScheduler>().CancelCurrentAction(); //사망시 행동 캔슬
        }

        // public JToken CaptureAsJToken()
        // {
        //     return JToken.FromObject(healthPoints);
        // }
        // public void RestoreFromJToken(JToken state)
        // {
        //     healthPoints = state.ToObject<float>();
        //     //UpdateState();
        // }

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