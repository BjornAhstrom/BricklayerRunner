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
        yield return new WaitForSeconds(2);

        GameObject player = Instantiate(playerPrefab);
        player.transform.position = startPosition.position;
    }








    ////[SerializeField] private Transform position;

    //private List<GameObject> players = new List<GameObject>();

    //private bool stop = false;

    //private void Start()
    //{
    //    if ( playerPrefabSpawner == null)
    //    {
    //        return;
    //    }
    //    SpawnPlayer();
    //}

    //private void Update()
    //{

    //}

    //private void SpawnPlayer()
    //{
    //    GameObject player = Instantiate(playerPrefabSpawner, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

    //    players.Add(player);

    //    PlayerController playerController = player.GetComponentInChildren<PlayerController>();
    //    playerController.playerSpawnerController = this;
    //}

    //void DestroyPlayerPrefab(GameObject player)
    //{
    //    players.Remove(player);

    //    Destroy(player);
    //}

    //public void Stop()
    //{
    //    Debug.Log("Player DEAD");
    //    stop = true;

    //    for (int i = players.Count - 1; i >= 0; i--)
    //    {
    //        DestroyPlayerPrefab(players[i]);
    //    }
    //}
}
