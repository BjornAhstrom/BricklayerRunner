using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBeahviour : StateMachineBehaviour
{
    Transform playerTransform;
    EnemyController enemy;
    Rigidbody2D rb;

    public float followSpeed = 350f;
    int distansToPlayerHash = Animator.StringToHash("DistanceToPlayer");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject.GetComponent<EnemyController>();
        rb = enemy.GetComponent<Rigidbody2D>();

        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 current = animator.transform.position;
        Vector2 target = playerTransform.position;

        Vector2 direction = new Vector2((target - current).normalized.x, 0);

        rb.velocity = new Vector2(direction.x * followSpeed * Time.deltaTime, rb.velocity.y);

        //animator.transform.position = Vector2.MoveTowards(current, target, enemyFollowSpeed * Time.deltaTime);

        animator.SetFloat(distansToPlayerHash, Vector2.Distance(current, target));

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
