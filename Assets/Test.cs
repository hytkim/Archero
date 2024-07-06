using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject my;
    int cnt = 0;
    void Start()
    {
        my = gameObject;
    }
    public void EnemyDie()
    {
        cnt++;

        if (cnt >= 5)
        {
            Destroy(gameObject, 0.2f);

            StageMgr.Single.ClearBanner.SetActive(true);

            return;
        }
    }
}
