using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerController : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform startPosition;

    private void Start()
    {
        StartCoroutine(PlayerStartPosition());
    }

    IEnumerator PlayerStartPosition()
    {
        PlayerController.Instance.gameObject.SetActive(false);

        PlayerController.Instance.transform.position = startPosition.position;

        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.gameObject.SetActive(true);
    }
}
