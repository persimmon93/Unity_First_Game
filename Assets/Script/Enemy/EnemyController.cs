using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 5f;   //Detection range for player.


    Transform target;   //Reference to player.
    NavMeshAgent agent; //Reference to the NavMeshAgent.
    CharacterCombat combat;
    //By default isEnemy will be set to false.
    public bool isEnemy = true;

    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculates distance between player and itself.
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius && isEnemy)
        {
            //Moves towards target.
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    //Attack the target
                    combat.Attack(targetStats);
                }
                //Face the target
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void TurnEnemy()
    {
        isEnemy = true;
    }
}
