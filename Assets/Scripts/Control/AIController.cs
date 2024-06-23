using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.Control{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;

        Fighter fighter;
        Mover mover;
        GameObject player;
        Health health;
        Vector3 guardLocation;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start(){
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            guardLocation = transform.position;
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (health.IsDead()) return;
            if (InAttackRangeOfPlayer(player) && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
            else
            {
                mover.StartMoveAction(guardLocation);
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer(GameObject player)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //유니티에서 호출
        private void OnDrawGizmosSelected(){
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
