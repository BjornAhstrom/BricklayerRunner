using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] LayerMask enemyMask;
    [SerializeField] LayerMask groundMask;

    private void Update()
    {
        CheckIfHitEnemyWithBricks();
    }

    void CheckIfHitEnemyWithBricks()
    {
        Vector2 originBrick = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D leftSideBrick = Physics2D.Raycast(originBrick, Vector2.left, 1f, enemyMask, groundMask);
        RaycastHit2D rightSideBrick = Physics2D.Raycast(originBrick, Vector2.right, 1f, enemyMask, groundMask);
        RaycastHit2D upSideBrick = Physics2D.Raycast(originBrick, Vector2.up, 0.3f, enemyMask, groundMask);
        RaycastHit2D bottomSideBrick = Physics2D.Raycast(originBrick, Vector2.down, 0.3f, enemyMask, groundMask);
 
        if (leftSideBrick.collider != null || rightSideBrick.collider != null || upSideBrick.collider != null || bottomSideBrick.collider != null)
        {
            Debug.Log("Collide with ");
            gameObject.SetActive(false);
        }
    }
}
