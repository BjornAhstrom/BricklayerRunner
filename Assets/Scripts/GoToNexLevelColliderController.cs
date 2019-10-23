using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNexLevelColliderController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.Instance.SaveScoreToDevice();
            SceneHandler.Instance.ChangeLevelTo("Level2");
        }
    }
}
