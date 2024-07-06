using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMgr : MonoBehaviour
{
    public static StageMgr Single // singlton     
    {
        get
        {
            if (single == null)
            {
                single = FindObjectOfType<StageMgr>();
                if (single == null)
                {
                    var instanceContainer = new GameObject("StageMgr");
                    single = instanceContainer.AddComponent<StageMgr>();
                }
            }
            return single;
        }
    }
    private static StageMgr single;

    public GameObject Player;

    public GameObject ClearBanner;

    [System.Serializable]
    public class StartPositionArray
    {
        public List<Transform> StartPosition = new List<Transform>();
    }

    public StartPositionArray[] startPositionArrays;    // 0 1 2
    //startPositionArrays[0] 1~10 Stage
    //startPositionArrays[1] 11~20 Stage
    //방 20개 만들어서 각 방의 시작위치를 입력한다.

    public List<Transform> StartPositionAngel = new List<Transform>();
    // 천사방 3개 
    public List<Transform> StartPositionBoss = new List<Transform>();
    // 보스 3개
    public Transform StartPositionLastBoss;
    // 막보 하나

    public int currentStage = 0;  //현재 방위치
    int LastStage = 5; // 막보방

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ClearBanner.gameObject.SetActive(false);
    }

    public void NextStage()
    {
        currentStage++;
        if (currentStage > LastStage)
        {
            Debug.Log("End Game");
            return;
        }

        if (currentStage < 5)  //Normal Stage
        {
            int arrayIndex = currentStage / 10;
            int randomIndex = Random.Range(0, startPositionArrays[arrayIndex].StartPosition.Count);
            Player.transform.position = startPositionArrays[arrayIndex].StartPosition[randomIndex].position;
            startPositionArrays[arrayIndex].StartPosition.RemoveAt(randomIndex);
        }
        else    //BossRoom or Angel
        {
            Player.transform.position = StartPositionLastBoss.position;
        }

        /*else    //BossRoom or Angel
        {
            if ( currentStage % 10 == 5 )   // Angel
            {
                int randomIndex = Random.Range ( 0, StartPositionAngel.Count );
                Player.transform.position = StartPositionAngel[randomIndex].position;
            }
            else    //Boss
            {
                if ( currentStage == LastStage )  //LastBoss
                {
                    Player.transform.position = StartPositionLastBoss.position;
                }
                else    //Mid Boss
                {
                    int randomIndex = Random.Range ( 0, StartPositionBoss.Count );
                    Player.transform.position = StartPositionBoss[randomIndex].position;
                    StartPositionBoss.RemoveAt ( currentStage / 10 );
                }
            }
        }*/
        CameraMovement.Single.CarmeraNextRoom();
    }//NextStage
}
