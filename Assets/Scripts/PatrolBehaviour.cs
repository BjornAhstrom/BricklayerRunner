using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    public GameObject enemeyPointsPrefab;
    Transform enemyPatrolPoints;
    Transform playerTransform;

    public float speed = 3.0f;

    int randomPointsIndex;
    int playerDistanceHash = Animator.StringToHash("DistanceToPlayer");



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyPatrolPoints == null)
        {
            enemyPatrolPoints = Instantiate(enemeyPointsPrefab).transform;
        }

        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        randomPointsIndex = Random.Range(0, enemyPatrolPoints.childCount);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 current = animator.transform.position;
        Vector2 target = enemyPatrolPoints.GetChild(randomPointsIndex).position;
        Vector2 player = playerTransform.position;

        float playerDistance = Vector2.Distance(current, player);
        animator.SetFloat(playerDistanceHash, playerDistance);

        if (Vector2.Distance(current, target) > 0.1f)
        {
            animator.transform.position = Vector2.MoveTowards(current, target, speed * Time.deltaTime);
        }
        else
        {
            randomPointsIndex = Random.Range(0, enemyPatrolPoints.childCount);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
