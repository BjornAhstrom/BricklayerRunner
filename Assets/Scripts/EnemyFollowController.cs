using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowController : MonoBehaviour
{
    public Transform player;
    [Range(0, 10)]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movment;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        Vector3 scale = transform.localScale;
        float angle = Mathf.Atan2(direction.y = 0, direction.x); // * Mathf.Rad2Deg;

        if (player.position.x +1 >= transform.position.x)
        {
            Debug.Log("Right " + angle);
            scale.x = -0.1f;
        }
        else if (player.position.x <= transform.position.x)
        {
            Debug.Log("Left " + angle);
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

}
