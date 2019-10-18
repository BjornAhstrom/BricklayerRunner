using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] GameObject particle;

    private float destroyDelayWhenEnemyHit = 0.01f;
    private float destroyDelay = 0.01f;

    //[SerializeField] LayerMask layerMask;

    //[Range(0, 5)] public float raycastLengthRightAndLeftSide = 0.5f;
    //[Range(0, 5)] public float raycastLengtBottomAndTop = 0.1f;

    //private void Update()
    //{
    //CheckIfHit();
    // StartCoroutine(MakeBrickDisappear());
    //}

    //void CheckIfHit()
    //{
    //    Vector2 originBrick = new Vector2(transform.position.x, transform.position.y);
    //    RaycastHit2D leftSideBrick = Physics2D.Raycast(originBrick, Vector2.left, raycastLengthRightAndLeftSide, layerMask);
    //    RaycastHit2D rightSideBrick = Physics2D.Raycast(originBrick, Vector2.right, raycastLengthRightAndLeftSide, layerMask);
    //    RaycastHit2D upSideBrick = Physics2D.Raycast(originBrick, Vector2.up, raycastLengtBottomAndTop, layerMask);
    //    RaycastHit2D bottomSideBrick = Physics2D.Raycast(originBrick, Vector2.down, raycastLengtBottomAndTop, layerMask);

    //    if (leftSideBrick.collider != null || rightSideBrick.collider != null || upSideBrick.collider != null || bottomSideBrick.collider != null)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}

    //private void OnEnable()
    //{
    //    Debug.Log("brick enabled");
    //}

    private void Start()
    {
        particle.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            particle.SetActive(true);
            StartCoroutine(DealyHitWithEnemy());
        }
        else
        {
            //particle.SetActive(true);
            StartCoroutine(MakeBrickDisappear());
        }
    }

    IEnumerator DealyHitWithEnemy()
    {
        yield return new WaitForSeconds(destroyDelayWhenEnemyHit);
        gameObject.SetActive(false);
    }

    IEnumerator MakeBrickDisappear()
    {
        yield return new WaitForSeconds(destroyDelay);
        gameObject.SetActive(false);
    }
}
