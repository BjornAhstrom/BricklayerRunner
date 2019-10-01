using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowsBrickController : MonoBehaviour
{
    PlayerController playerController;

    [Range(500, 5000)] public float brickSpeed = 1000f;

    public GameObject brickPrefab;
    public int playersCurrentBricks = 0;
    public List<Rigidbody2D> bricks = new List<Rigidbody2D>();

    private GameObject brick;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

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
            rb.transform.position = transform.position;

            rb.AddForce(position * brickSpeed);
            playersCurrentBricks--;
            yield return 0;
        }
    }

    // Samlar tegelstenar från BricksController
    public void CollectBricks(int collectBricksamount)
    {
        playersCurrentBricks += collectBricksamount;
    }

}
