using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    namespace RPG.Combat {
        [CreateAssetMenu(fileName = "Weapon", menuName = "RPG Project/Make New Weapon", order = 0)]
        public class Weapon : ScriptableObject {
            [SerializeField] GameObject equippedPrefab = null;
            [SerializeField] AnimatorOverrideController weaponOverride = null;
            [SerializeField] float weaponRange = 4f;
            [SerializeField] float timeBetweenAttacks = 1.5f;
            [SerializeField] float weaponDamage = 25f;

            public void Spawn(Transform handTransform, Animator animator){
                if(equippedPrefab != null){
                    Instantiate(equippedPrefab, handTransform);
                }
                if(weaponOverride != null){
                    animator.runtimeAnimatorController = weaponOverride;
                }

            }

            public float GetDamage(){
                return weaponDamage;
            }

            public float getRange(){
                return weaponRange;
            }
        }
    }
