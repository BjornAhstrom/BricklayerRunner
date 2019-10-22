using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBehaviour : StateMachineBehaviour
{
    EnemyController enemy;
    Rigidbody2D rb;

    [Range(0, 10)] public float firstStopLengthTime = 1f;
    [Range(0, 10)] public float secondStopLengthTime = 3f;

    int stopHash = Animator.StringToHash("Stop");
    int runHash = Animator.StringToHash("Run");

    float timeToStop;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject.GetComponent<EnemyController>();
        rb = enemy.GetComponent<Rigidbody2D>();

        timeToStop = Random.Range(firstStopLengthTime, secondStopLengthTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeToStop -= Time.deltaTime;

        if (timeToStop <= 0)
        {
            animator.SetBool(runHash, true);
        }

        rb.velocity = new Vector2(0, 0);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(stopHash, false);
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
