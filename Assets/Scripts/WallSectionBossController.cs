using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSectionBossController : MonoBehaviour
{
    [SerializeField] List<GameObject> wallSections = new List<GameObject>();

    //private void Start()
    //{
    //    StartCoroutine(MakeWallDisappear());
    //}

    public void StartBreakingTheWall()
    {
        StartCoroutine(MakeWallDisappear());
    }

    IEnumerator MakeWallDisappear()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < wallSections.Count; i++)
        {
            for (int j = 0; j < wallSections.Count; j++)
            {
                yield return new WaitForSeconds(0.2f);
                wallSections[i].gameObject.SetActive(false);
                wallSections[j].gameObject.SetActive(false);
            }
        }
    }
}
