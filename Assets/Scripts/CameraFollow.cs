using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;

    [Range(0, 1)] public float smoothDampTime = 0.15f;
    [Range(0, 10)] public float cameraOrthographicSize = 2.1f;
    [Range(-10, 10)] public float cameraMinPlayerYBottomCameraPosition = 0.05f;
    [Range(-10, 10)] public float cameraMaxPlayerYTopCameraPosition = 0.05f;

    public Transform leftBounds;
    public Transform rightBounds;

    private Vector3 smoothDampVelocity = Vector3.zero;
    private int splitScreen = 2;
    private float camWidth;
    private float camHeigt;
    private float levelMinX;
    private float levelMaxX;
    private float leftBoundWidth;
    private float rightBoundsWidth;
    private float playerX;
    private float playerY;
    private float x;
    private float y;

    private void Start()
    {
        camHeigt = Camera.main.orthographicSize * cameraOrthographicSize;
        camWidth = camHeigt * Camera.main.aspect;

        leftBoundWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / splitScreen;
        rightBoundsWidth = rightBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / splitScreen;

        levelMinX = leftBounds.position.x + leftBoundWidth + (camWidth / splitScreen);
        levelMaxX = rightBounds.position.x + rightBoundsWidth - (camWidth / splitScreen);
    }

    private void Update()
    {
        if (player)
        {
            playerX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, player.position.x));

            x = Mathf.SmoothDamp(transform.position.x, playerX, ref smoothDampVelocity.x, smoothDampTime);

            playerY = Mathf.Max(cameraMinPlayerYBottomCameraPosition, Mathf.Min(levelMaxX, player.position.y));

            y = Mathf.SmoothDamp(transform.position.y, playerY, ref smoothDampVelocity.y, smoothDampTime);

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}