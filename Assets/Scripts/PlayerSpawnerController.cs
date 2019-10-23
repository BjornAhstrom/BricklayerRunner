using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerController : MonoBehaviour
{
    [SerializeField] Transform checkPoint1;
    [SerializeField] Transform checkPoint2;
    [SerializeField] Transform startPosition;

    Vector2 currentCheckPointPosition;

    private YieldInstruction startDelay = new WaitForSeconds(0.2f);

    private void Start()
    {
        currentCheckPointPosition = startPosition.position;

        if (PlayerController.Instance.runTroughCheckPoint1)
        {
            currentCheckPointPosition = checkPoint1.position;
        }
        else if (PlayerController.Instance.runTroughCheckPoint2)
        {
            currentCheckPointPosition = checkPoint2.position;
        }
        else
        {
            currentCheckPointPosition = startPosition.position;
        }

        StartCoroutine(PlayerStartPosition());
    }

    IEnumerator PlayerStartPosition()
    {
        PlayerController.Instance.gameObject.SetActive(false);

        PlayerController.Instance.transform.position = currentCheckPointPosition;

        PlayerController.Instance.MakePlayerSmaller();

        yield return startDelay;

        PlayerController.Instance.gameObject.SetActive(true);
    }
}
