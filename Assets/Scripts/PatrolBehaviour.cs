using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    Transform playerTransform;
    EnemyController enemy;

    public float patrolSpeed = 250f;

    int randomPointsIndex;
    int playerDistanceHash = Animator.StringToHash("DistanceToPlayer");
    int stopHash = Animator.StringToHash("Stop");
    float timeStillWalking;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject.GetComponent<EnemyController>();

        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        randomPointsIndex = Random.Range(0, enemy.positions.childCount);
        if (rb == null)
        {
            rb = animator.gameObject.GetComponent<Rigidbody2D>();
        }

        rb.velocity = Vector3.zero;

        timeStillWalking = Random.Range(3f, 6f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 current = animator.transform.position;
        Vector2 target = new Vector2( enemy.positions.GetChild(randomPointsIndex).position.x, current.y);

        Vector2 direction = new Vector2((target - current).normalized.x, 0);
        Vector2 player = playerTransform.position;

        float playerDistance = Vector2.Distance(current, player);
        animator.SetFloat(playerDistanceHash, playerDistance);
       
        if (Vector2.Distance(current, target) > 0.1f)
        {
            rb.velocity = new Vector2(direction.x * patrolSpeed * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            randomPointsIndex = Random.Range(0, enemy.positions.childCount);
        }

        timeStillWalking -= Time.deltaTime;

        if (timeStillWalking <= 0)
        {
            animator.SetBool(stopHash, true);
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
