using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteMgr : MonoBehaviour
{
    public GameObject RoulettePlate;//실제로 회전하게될 룰렛 본체
    public GameObject RoulettePanel;//룰렛 화면 On/Off를 위한 룰렛 페널
    public Transform Needle;//룰렛이 멈췄을때, 스킬 아이콘과 거리를 확인하기위한 바늘 

    public Sprite[] SkillSprite;// 스킬아이콘으로사용될 스프라이트
    public Image[] DisplayItemSlot;// 매번 바뀔 스킬 아이콘들이 들어갈 슬롯 이미지

    List<int> StartList = new List<int>();//겹치지 않은 랜덤뽑기를 위한 리스트
    List<int> ResultIndexList = new List<int>();//당첨된 스킬의 인덱스를 저장할 리스트
    int ItemCnt = 6;//룰렛 위에 올라갈 총 스킬 갯수

    private void Start()
    {
        //Set Random Skill Icon  1. 리스트에 데이터 등록
        for (int i = 0; i < ItemCnt; i++)
        {
            StartList.Add(i);
        }
        //Set Random Skill Icon  2. 리스트에 등록된 데이터에 무작위로 접근하여 슬롯에 하나씩 적용
        for (int i = 0; i < ItemCnt; i++)
        {
            int randomIndex = Random.Range(0, StartList.Count);
            ResultIndexList.Add(StartList[randomIndex]);
            DisplayItemSlot[i].sprite = SkillSprite[StartList[randomIndex]];
            StartList.RemoveAt(randomIndex);//중복 스킬이 룰렛에 올라가지못하도록 뽑힌 데이터를 시작 리스트에서 제거
        }
        
        StartCoroutine(StartRoulette());
    }
    IEnumerator StartRoulette()
    {
        yield return new WaitForSeconds(2f);
        float randomSpd = Random.Range(1.0f, 5.0f);
        float rotateSpeed = 100f * randomSpd;
        // Rotate Roulette 1. 무한반복문으로 Lerp함수를 돌려서 랜덤한 속도로 회전하는 룰렛
        while (true)
        {
            yield return null;
            if (rotateSpeed <= 0.01f)
                break;

            rotateSpeed = Mathf.Lerp(rotateSpeed, 0, Time.deltaTime * 2f);
            RoulettePlate.transform.Rotate(0, 0, rotateSpeed);
        }

        yield return new WaitForSeconds(1f);
        Result();

        yield return new WaitForSeconds(1f);
        //RoulettePanel.SetActive ( false );
    }
    // Set Slot Item  1.바늘과 모든 슬롯의 위치를 계산하여 가장 가까운 인덱스를 찾아내고, 해당 인덱스의 아이템을 가져옴
    void Result()
    {
        int closetIndex = -1;
        float closetDis = 500f;
        float currentDis = 0f;

        for (int i = 0; i < ItemCnt; i++)
        {
            currentDis = Vector2.Distance(DisplayItemSlot[i].transform.position, Needle.position);
            if (closetDis > currentDis)
            {
                closetDis = currentDis;
                closetIndex = i;
            }
        }
        Debug.Log(" closetIndex : " + closetIndex);
        if (closetIndex == -1)
        {
            Debug.Log("Something is wrong!");
        }
        DisplayItemSlot[ItemCnt].sprite = DisplayItemSlot[closetIndex].sprite;

        Debug.Log(" LV UP Index : " + ResultIndexList[closetIndex]);
    }

}
