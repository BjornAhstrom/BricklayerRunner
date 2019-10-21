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
    //int runHash = Animator.StringToHash("Run");
    int runRightHash = Animator.StringToHash("RunRight");
    int runLeftHash = Animator.StringToHash("RunLeft");

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

        if (enemy.runLeft == true)
        {
            animator.SetBool(runLeftHash, true);
        }
        else if (enemy.runRight == true)
        {
            animator.SetBool(runRightHash, true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(distansToPlayerHash, 0);
        animator.SetBool(runLeftHash, false);
        animator.SetBool(runRightHash, false);
    }

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
