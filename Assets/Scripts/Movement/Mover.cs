using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent navMeshAgent;
        Health health;

        private void Start(){
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if(health.IsDead()) navMeshAgent.enabled = false;

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination){
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }


        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

         public void Cancel(){
            navMeshAgent.isStopped = true;
        }
        

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("Forward Speed", speed);
        }
    }


}
