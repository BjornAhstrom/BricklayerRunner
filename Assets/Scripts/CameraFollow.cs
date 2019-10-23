using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraFollow : MonoBehaviour
{
    [Range(0, 1)] public float smoothDampTime = 0.15f;
    [Range(0, 10)] public float cameraOrthographicSize = 0f;
    [Range(-10, 10)] public float cameraMinPlayerYBottomCameraPosition = 0.05f;
    [Range(-10, 10)] public float cameraMaxPlayerYTopCameraPosition = 0.05f;
    [Range(-100, 100)] public float freezePositionX = 0f;
    [Range(-100, 100)] public float freezePositionY = 50f;
    [Range(-30, 30)] public float zoomOutTo = 9f;
    [Range(0, 10)] public float zoomOutTime = 2f;

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
    private bool freezeCameraPosition;

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
        CameraFollowSmooth();
    }

    void CameraFollowSmooth()
    {
        if (PlayerController.Instance && !freezeCameraPosition)
        {
            playerX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, PlayerController.Instance.transform.position.x));

            x = Mathf.SmoothDamp(transform.position.x, playerX, ref smoothDampVelocity.x, smoothDampTime);

            playerY = Mathf.Max(cameraMinPlayerYBottomCameraPosition, Mathf.Min(levelMaxX, PlayerController.Instance.transform.position.y));

            y = Mathf.SmoothDamp(transform.position.y, playerY, ref smoothDampVelocity.y, smoothDampTime);

            transform.position = new Vector3(x, y, transform.position.z);
        }
        else
        {
            Vector3 pos = new Vector3(freezePositionX, freezePositionY, Camera.main.transform.position.z);

            Camera.main.transform.position = pos;
        }
    }

     public void ZoomOut()
    {
        freezeCameraPosition = true;

        LeanTween.value(gameObject, Camera.main.orthographicSize, zoomOutTo, zoomOutTime).setOnUpdate((float test) =>
        {
            Camera.main.orthographicSize = test;
        });
    }
}