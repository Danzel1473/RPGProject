using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat{

    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] float lifeAfterEmpact = 2f;

        [SerializeField] GameObject[] destroyOnHit = null;



        Health target = null;
        float damage = 0;
        
        private void Start() {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if(target == null) { return; }
            if(isHoming && !target.IsDead()){
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage){
            this.target = target;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        public void OnTriggerEnter(Collider other){
            if(other.GetComponent<Health>() != target) return;
            if(target.IsDead()) return;
            target.TakeDamage(damage);

            speed = 0; //피격 후 멈추기

            if(hitEffect != null){
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach(GameObject toDestroy in destroyOnHit) { //정해진 오브젝트만 Destroy
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterEmpact); //일정 시간 후 전체 Destroy
        }
    }

}