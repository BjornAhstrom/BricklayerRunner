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
        yield return new WaitForSeconds(0.2f);

        GameObject player = Instantiate(playerPrefab);
        player.transform.position = startPosition.position;
    }
}
