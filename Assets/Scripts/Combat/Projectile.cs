using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat{

    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        Health target = null;
        float damage = 0;


        void Update()
        {
            if(target == null) { return; }
            transform.LookAt(GetAiRmLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage){
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAiRmLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        public void OnTriggerEnter(Collider other){
            if(other.GetComponent<Health>() != target) return;

            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}