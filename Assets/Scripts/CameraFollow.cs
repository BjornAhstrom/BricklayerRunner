using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public Transform leftBounds;
    public Transform rightBounds;
    public Transform bottomBounds;
    public Vector3 offset;

    [Range(0, 1)]
    public float smoothDampTime = 0.30f;
    [Range(0, 10)]
    public float orthographicSize = 2;
    [Range(0, 1)]
    public float minPlayerYBottomCameraPosition = 0.05f;
    [Range(0, 1)]
    public float maxPlayerYTopCameraPosition = 0.05f;

    private Vector3 SmoothDampVelocity = Vector3.zero;

    private float camWidth, camHeight, levelMin, levelMax; // levelMinBottom;

    // Start is called before the first frame update
    void Start()
    {
        camHeight = Camera.main.orthographicSize * orthographicSize;
        camWidth = camHeight * Camera.main.aspect;

        float leftBoundsWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float rightBoundsWidth = rightBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        //float bottomBoundsWidth = bottomBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;

        levelMin = leftBounds.position.x + leftBoundsWidth + (camWidth / 2);
        levelMax = rightBounds.position.x - rightBoundsWidth - (camWidth / 2);
        //levelMinBottom = bottomBounds.position.y + bottomBoundsWidth + (camHeight / 2);

        //levelHeigtMin = bottomBounds.position.y - bottomBoundsWidth - (camHeight / 2);

    }

    // Update is called once per frame
    void Update()
    {
        //CameraUpdatePositions();
        CameraFollowUpdatePosition();
    }

    void CameraUpdatePositions()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);

    }

    void CameraFollowUpdatePosition()
    {
        if (player)
        {
            float playerX = Mathf.Max(levelMin, Mathf.Min(levelMax, player.position.x));

            float x = Mathf.SmoothDamp(transform.position.x, playerX, ref SmoothDampVelocity.x, smoothDampTime);


            float playerY = Mathf.Max(minPlayerYBottomCameraPosition, Mathf.Min(levelMax, player.position.y));

            float y = Mathf.SmoothDamp(transform.position.y, playerY, ref SmoothDampVelocity.y, smoothDampTime);


            transform.position = new Vector3(x, y, transform.position.z);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Enemy"))
        {
            Debug.Log("Enemy inside camera view");
        }
    }

}
