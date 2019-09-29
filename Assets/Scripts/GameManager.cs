using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector2 playerMoveVelocity;
    public LayerMask groundMask;
    public LayerMask enemyMask;
    public LayerMask playerLayerMask;

    [Range(0, 20)] public float playerMaxSpeed = 10f;
    [Range(0, 30)] public float playerJumpVelocity = 17f;
    [Range(0, 0.01f)] public float playerHealthBarStatusSpeed = 0.005f;
    [Range(0, 10)] public float playerDistanceToGround = 1.2f;
    [Range(0, 5)] public float playerHitDistanceLeftAndRightSideOn = 1.2f;

    [Range(0, 10)] public float enemyMoveSpeed = 5f;

    //[Range(0, 1)] public float cameraSmoothDampTime = 0.30f;
    //[Range(0, 10)] public float camerOrthographicSize = 2.1f;
    //[Range(-10, 10)] public float cameraMinPlayerYBottomCameraPosition = 0.05f;
    //[Range(-10, 10)] public float cameraMaxPlayerYTopCameraPosition = 0.05f;

}
