﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunFromPlayerBehaviour : StateMachineBehaviour
{
    EnemyController enemy;
    Rigidbody2D rb;

    [Range(0, 1000)] public float runFromPlayerSpeed = 700f;
    [Range(0, 5)] public float firstRunLenghtTime = 0.5f;
    [Range(0, 5)] public float secondRunLenghtTime = 1f;

    private int runRightHash = Animator.StringToHash("RunRight");
    private int runLeftHash = Animator.StringToHash("RunLeft");
    private float runTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Run from playerBehaviour");

        enemy = animator.gameObject.GetComponent<EnemyController>();
        rb = enemy.GetComponent<Rigidbody2D>();

        runTime = Random.Range(firstRunLenghtTime, secondRunLenghtTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        runTime -= Time.deltaTime;

        RunFromPlayerLeftOrRight();

        WhenRunTimeHasRunOut(animator, false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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

    void RunFromPlayerLeftOrRight()
    {
        if (enemy.runLeft && !enemy.runRight)
        {
            Vector2 direction = new Vector2(Vector2.left.normalized.x, 0);

            rb.velocity = new Vector2(direction.x * runFromPlayerSpeed * Time.deltaTime, rb.velocity.y);
        }
        else if (enemy.runRight && !enemy.runLeft)
        {
            Vector2 direction = new Vector2(Vector2.right.normalized.x, 0);

            rb.velocity = new Vector2(direction.x * runFromPlayerSpeed * Time.deltaTime, rb.velocity.y);
        }
    }

    void WhenRunTimeHasRunOut(Animator animator, bool boolValue)
    {
        if (runTime <= 0)
        {
            enemy.runLeft = boolValue;
            enemy.runRight = boolValue;
            animator.SetBool(runLeftHash, boolValue);
            animator.SetBool(runRightHash, boolValue);
        }
    }
}
