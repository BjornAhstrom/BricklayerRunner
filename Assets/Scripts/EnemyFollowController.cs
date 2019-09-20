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

    //public Transform position;
    [SerializeField]
    List<Transform> positions = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCollideWithPlayer();

        Vector3 direction = player.position - transform.position;
        Vector3 scale = transform.localScale;
        float angle = Mathf.Atan2(direction.y = 0, direction.x); // * Mathf.Rad2Deg;

        if (player.position.x +1 >= transform.position.x)
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

        rb.rotation = angle;
        direction.Normalize();
        movment = direction;
        rb.gravityScale = 10;
    }

    private void FixedUpdate()
    {
        MoveEnemy(movment);
    }

    void MoveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    if (collision.gameObject.name.Equals("Player"))
    //    {
    //        Debug.Log("Collide with " + collision.gameObject.name + " : " + gameObject.name);
    //    }


    //}

    void CheckIfCollideWithPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 1f, layerMask);

        if (hit.collider != null)
        {
            Debug.Log("Hit ");
        }
        
    }
}
