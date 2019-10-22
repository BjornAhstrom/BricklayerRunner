﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectedBricksController : MonoBehaviour
{
    [SerializeField] PlayerThrowsBrickController playerThrowsBrickController;

    TextMeshPro collectedBricksText;
    public Vector2 offset;

    public int bricksAmount = 0;

    private Vector2 mainCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (playerThrowsBrickController == null)
        {
            playerThrowsBrickController = GetComponent<PlayerThrowsBrickController>();
        } 
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
        mainCameraPosition = Camera.main.transform.position;
        transform.position = new Vector2(mainCameraPosition.x + offset.x, mainCameraPosition.y + offset.y);
    }
}
