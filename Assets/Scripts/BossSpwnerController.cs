using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpwnerController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPos;
    public GameObject wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        wall.SetActive(true);

        Camera.main.GetComponent<CameraFollow>().ZoomOut();

        // if collision is player
        if (collision.CompareTag("Player"))
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.GetComponent<EnemyController>().positions = transform.GetChild(0);

            enemy.transform.position = spawnPos.position;

            gameObject.SetActive(false);
        }
    }
}
