using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectedBricksController : MonoBehaviour
{
    [SerializeField] PlayerThrowsBrickController playerThrowsBrickController;

    public Vector2 offset;
    public CameraFollow camera;
    TextMeshPro collectedBricksText;
    public int bricksAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        collectedBricksText = GetComponentInChildren<TextMeshPro>();
        collectedBricksText.text = bricksAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CollectedBricksÍmageAndTextSetTopRight();
        UpdateBricksAmountText();
    }

    void UpdateBricksAmountText()
    {
        bricksAmount = playerThrowsBrickController.playersCurrentBricks;
        collectedBricksText.text = bricksAmount.ToString();
    }

    void CollectedBricksÍmageAndTextSetTopRight()
    {
        transform.position = new Vector2(camera.transform.position.x + offset.x, camera.transform.position.y + offset.y);

    }
}
