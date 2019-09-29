using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] GameObject enemySpawnerPrefab;
    [SerializeField] List<Transform> positions = new List<Transform>();

    private List<GameObject> enemies = new List<GameObject>();

    Vector3 mainCameraCurrentPosition;

    private float leftSideCameraFrame = -12f;
    private float rightSideCameraFrame = 12f;
    private float topCameraFrame = 7f;
    private float bottomCameraFrame = 7f;

    private bool stop = false;

    private void Start()
    {
        mainCameraCurrentPosition = Camera.main.transform.position;

        

        if (enemySpawnerPrefab == null)
        {
            return;
        }
    }

    private void Update()
    {
        CheckIfEnemyIsInsideCameraView();
    }

    // if camera view is touching the enemy position point, the enemy will arise
    void CheckIfEnemyIsInsideCameraView()
    {
        mainCameraCurrentPosition = Camera.main.transform.position;

        //Debug.Log("Camera position x " + mainCameraCurrentPosition.x + " Camera position y " + mainCameraCurrentPosition.y + " Camera position z " + mainCameraCurrentPosition.z);

        for (int i = 0; i < positions.Count; i++)
        {
            if (positions[i].position.x > (mainCameraCurrentPosition.x + leftSideCameraFrame) && positions[i].position.x < (mainCameraCurrentPosition.x + rightSideCameraFrame) && positions[i].position.y > (mainCameraCurrentPosition.y - bottomCameraFrame) && positions[i].position.y < (mainCameraCurrentPosition.y + topCameraFrame) && enemies.Count < 1 && stop == false)
            {
                SpawnEnemy(i);
            }
        }
        stop = false;
    }

    private void SpawnEnemy(int index)
    {
        // Creats an enemy clone on a spesific position
        GameObject enemy = Instantiate(enemySpawnerPrefab, new Vector3(positions[index].position.x, positions[index].position.y, 0), Quaternion.identity);

        // Adds clones to the list
        enemies.Add(enemy);
        EnemyFollowController enemyFollowController = enemy.GetComponentInChildren<EnemyFollowController>();

        //enemyFollowController.enemySpawnerController = this;
    }

    void DestroyEnemyClone(GameObject enemy)
    {
        // Removes enemy from the list
        enemies.Remove(enemy);

        // Destroy enemy
        Destroy(enemy);
    }

    public void Stop()
    {
        stop = true;

        // Goes through the list from below and destroys the clones
        for (int i  = enemies.Count - 1; i >= 0; i--)
        {
            DestroyEnemyClone(enemies[i]);
        }
    }

}
