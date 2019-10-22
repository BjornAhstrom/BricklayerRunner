using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowsBrickController : MonoBehaviour
{
    public GameObject brickPrefab;
    public List<Rigidbody2D> bricks = new List<Rigidbody2D>();

    private GameObject brick;

    [Range(500, 5000)] public float brickSpeed = 1000f;

    public int playersCurrentBricks = 0;
    
    public void initializeBrickPrefabObject()
    {
            for (int i = 0; i < playersCurrentBricks; i++)
            {
                brick = Instantiate(brickPrefab);
                brick.SetActive(false);

                Rigidbody2D rb = brick.GetComponent<Rigidbody2D>();
                bricks.Add(rb);
        }
    }

    Rigidbody2D GetBrick()
    {
        foreach (Rigidbody2D brickRB in bricks)
        {
            if (!brickRB.gameObject.activeInHierarchy)
            {
                brickRB.gameObject.SetActive(true);
                return brickRB;
            }
        }
        return null;
    }
    
    public IEnumerator ThrowsBricks(Vector2 position)
    {
        
        Rigidbody2D rb = GetBrick();

        if (rb != null && playersCurrentBricks > 0)
            {
            rb.transform.position = PlayerController.Instance.transform.position;
           

            rb.AddForce(position * brickSpeed);
            playersCurrentBricks--;

            yield return 0;
            brickPrefab.gameObject.SetActive(false);
        }
    }

    // Collect bricks from BricksController
    public void CollectBricks(int collectBricksamount)
    {
        playersCurrentBricks += collectBricksamount;
    }

}
