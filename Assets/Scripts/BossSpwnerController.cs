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

        ZoomOut();

        // if collision is player
        if (collision.CompareTag("Player"))
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.GetComponent<EnemyController>().positions = transform.GetChild(0);

            enemy.transform.position = spawnPos.position;

            gameObject.SetActive(false);
        }
    }

    void ZoomOut()
    {
        LeanTween.value(Camera.main.gameObject, Camera.main.orthographicSize, 9f, 2f).setOnUpdate((float test) =>
        {
            Camera.main.orthographicSize = test;
        });
    }
}
