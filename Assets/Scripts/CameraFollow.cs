using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public Transform leftBounds;
    public Transform rightBounds;

    public float smoothDampTime = 0.15f;

    private Vector3 SmoothDampVelocity = Vector3.zero;

    private float camWidth, camHeight, levelMin, levelMax;

    // Start is called before the first frame update
    void Start()
    {
        camHeight = Camera.main.orthographicSize * 2;
        camWidth = camHeight * Camera.main.aspect;

        float leftBoundsWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float tightBoundsWidth = rightBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;

        levelMin = leftBounds.position.x + leftBoundsWidth + (camWidth / 2);
        levelMax = rightBounds.position.x - tightBoundsWidth - (camWidth / 2);

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            float playerX = Mathf.Max(levelMin, Mathf.Min(levelMax, player.position.x));

            float x = Mathf.SmoothDamp(transform.position.x, playerX, ref SmoothDampVelocity.x, smoothDampTime);

            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        
    }

}
