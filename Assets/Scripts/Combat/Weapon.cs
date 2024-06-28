using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

    namespace RPG.Combat {
        [CreateAssetMenu(fileName = "Weapon", menuName = "RPG Project/Make New Weapon", order = 0)]
        public class Weapon : ScriptableObject {
            [SerializeField] GameObject equippedPrefab = null;
            [SerializeField] AnimatorOverrideController weaponOverride = null;
            [SerializeField] float weaponRange = 4f;
            [SerializeField] float timeBetweenAttacks = 1.5f;
            [SerializeField] float weaponDamage = 25f;
            [SerializeField] bool isRightHanded = true;
            [SerializeField] Projectile projectile = null;

            const string weaponName = "Weapon";

            public void Spawn(Transform rightHand, Transform leftHand, Animator animator) {
                DestroyOldWeapon(rightHand, leftHand);

                if(equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);

                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = weaponName;
            }
            if (weaponOverride != null){
                    animator.runtimeAnimatorController = weaponOverride;
                }
            }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null) {
              oldWeapon = leftHand.Find(weaponName);  
            }
            if(oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public bool HasProjectile(){
                return projectile != null;
            }

            public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target){
                Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand,leftHand).position, Quaternion.identity);
                projectileInstance.SetTarget(target, weaponDamage);
            }

            public float GetDamage(){
                return weaponDamage;
            }

            public float getRange(){
                return weaponRange;
            }
        }
    }
