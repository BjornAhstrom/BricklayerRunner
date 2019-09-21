using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowController : MonoBehaviour
{
    [HideInInspector]
    public EnemySpawnerController enemySpawnerController;
    public LayerMask layerMask;
    public Transform player;
    [Range(0, 10)]
    public float moveSpeed = 5f;
   
    private Rigidbody2D rb;
    private Vector2 movment;

    [SerializeField]
    PlayerController playerController;
    //[SerializeField]
    //List<Transform> positions = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCollideWithPlayer();
        EnemyFollowThePlayer();
        
    }

    private void FixedUpdate()
    {
        MoveEnemy(movment);
    }

    // When player comes in the position of the enemy, the enemy will shows up and hunt the player.
    // if player is on the right side of the enemy the scale.x (angle) will be set to -0.1f, to the left scale.x will be set to 0.1f
    void EnemyFollowThePlayer()
    {
        Vector3 direction = player.position - transform.position;
        Vector3 scale = transform.localScale;
        float angle = Mathf.Atan2(direction.y = 0, direction.x); // * Mathf.Rad2Deg;

        if (player.position.x + 1 >= transform.position.x)
        {
            //Debug.Log("Right " + angle);
            scale.x = -0.1f;
        }
        else if (player.position.x <= transform.position.x)
        {
            //Debug.Log("Left " + angle);
            scale.x = 0.1f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.identity;

        rb.rotation = angle;
        direction.Normalize();
        movment = direction;
        rb.gravityScale = 10;
    }

    void MoveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    // If player jump on enemy, the enemy will disappear
    void CheckIfCollideWithPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, layerMask);

        if (hit.collider != null)
        {
            Debug.Log("DEAD ");
            enemySpawnerController.Stop();
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log("invinsible ");
    }
}
