using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    [Range(0, 5)] public float raycastLengthRightAndLeftSide = 1f;
    [Range(0, 5)] public float raycastLengtBottomAndTop = 0.5f;

    private void Update()
    {
       //CheckIfHit();
    }

    void CheckIfHit()
    {
        Vector2 originBrick = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D leftSideBrick = Physics2D.Raycast(originBrick, Vector2.left, raycastLengthRightAndLeftSide, layerMask);
        RaycastHit2D rightSideBrick = Physics2D.Raycast(originBrick, Vector2.right, raycastLengthRightAndLeftSide, layerMask);
        RaycastHit2D upSideBrick = Physics2D.Raycast(originBrick, Vector2.up, raycastLengtBottomAndTop, layerMask);
        RaycastHit2D bottomSideBrick = Physics2D.Raycast(originBrick, Vector2.down, raycastLengtBottomAndTop, layerMask);
 
        if (leftSideBrick.collider != null || rightSideBrick.collider != null || upSideBrick.collider != null || bottomSideBrick.collider != null)
        {
            //Debug.Log("Hit");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.IsTouchingLayers(layerMask))
        {
            gameObject.SetActive(false);
        }
    }
}
