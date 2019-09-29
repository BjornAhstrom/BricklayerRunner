using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] Transform player;
   
    [Range(-10, 10)] public float xLed = 0f;
    [Range(-10, 10)] public float yLed = 0f;
    [Range(-10, 10)] public float zLed = -10f;
    [Range(-10, 10)] public float levelMin;
    [Range(-10, 10)] public float levelMax;

    [Range(0, 1)] public float cameraSmoothDampTime = 0.30f;
    [Range(0, 10)] public float camerOrthographicSize = 2.1f;
    [Range(-10, 10)] public float cameraMinPlayerYBottomCameraPosition = 0.05f;
    [Range(-10, 10)] public float cameraMaxPlayerYTopCameraPosition = 0.05f;

    //float camHeight;
    //float camWidth;

    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private Vector3 offset;
    private Vector3 smoothDampVelocity = Vector3.zero;

    private void Start()
    {
    }

    private void Update()
    {
        offset.x = xLed;
        offset.y = yLed;
        offset.z = zLed - 10;

        CameraUpdatePositionFromPlayer();


        //Vector3 targetPosition = player.TransformPoint(new Vector3(5, 5, -10));

        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        
    }

    void CameraUpdatePositionFromPlayer()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
    }














    //void CameraFollowUpdatePosition()
    //{
    //    if (player)
    //    {
    //        float playerX = Mathf.Max(levelMin, Mathf.Min(levelMax, player.position.x));

    //        float x = Mathf.SmoothDamp(transform.position.x, playerX, ref smoothDampVelocity.x, cameraSmoothDampTime);


    //        float playerY = Mathf.Max(cameraMinPlayerYBottomCameraPosition, Mathf.Min(levelMax, player.position.y));

    //        float y = Mathf.SmoothDamp(transform.position.y, playerY, ref smoothDampVelocity.y, cameraSmoothDampTime);


    //        transform.position = new Vector3(x, y, transform.position.z);

    //    }
    //}




    //private Vector3 SmoothDampVelocity = Vector3.zero;

    //private float camWidth, camHeight, levelMin, levelMax, levelMaxTop, levelMinBottom;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    camHeight = Camera.main.orthographicSize * gameManager.camerOrthographicSize;
    //    camWidth = camHeight * Camera.main.aspect;

    //    float leftBoundsWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
    //    float rightBoundsWidth = rightBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
    //    float topBoundsHeigth = topBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;
    //    float bottomBoundsHeigt = bottomBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;

    //    levelMin = leftBounds.position.x + leftBoundsWidth + (camWidth / 2);
    //    levelMax = rightBounds.position.x - rightBoundsWidth - (camWidth / 2);

    //    levelMaxTop = topBounds.position.y + topBoundsHeigth + (camHeight / 2);
    //    levelMinBottom = bottomBounds.position.y - bottomBoundsHeigt - (camHeight / 2);


    //}
}
