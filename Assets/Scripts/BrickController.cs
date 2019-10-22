using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] GameObject particle;

    private float destroyDelayWhenEnemyHit = 0.01f;
    private float destroyDelay = 0.01f;

    private void Start()
    {
        particle.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            particle.SetActive(true);
            StartCoroutine(DealyHitWithEnemy());
        }
        else
        {
            //particle.SetActive(true);
            StartCoroutine(MakeBrickDisappear());
        }
    }

    IEnumerator DealyHitWithEnemy()
    {
        yield return new WaitForSeconds(destroyDelayWhenEnemyHit);
        gameObject.SetActive(false);
    }

    IEnumerator MakeBrickDisappear()
    {
        yield return new WaitForSeconds(destroyDelay);
        gameObject.SetActive(false);
    }
}
