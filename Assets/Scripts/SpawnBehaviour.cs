using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : StateMachineBehaviour
{
    EnemyController enemy;
    Rigidbody2D rb;

    int startWalkHash = Animator.StringToHash("StartWalk");
    float timeToStandStill;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject.GetComponent<EnemyController>();
        rb = enemy.GetComponent<Rigidbody2D>();

        //enemy.enemyIsAlive = false;
        //rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        timeToStandStill = 5f;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeToStandStill -= Time.deltaTime;

        if (timeToStandStill <= 0)
        {
            //enemy.enemyIsAlive = true;

            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;
            animator.SetBool(startWalkHash, true);
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
