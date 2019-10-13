using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    private void Update()
    {
        transform.position = PlayerController.Instance.transform.position;
    }
}
