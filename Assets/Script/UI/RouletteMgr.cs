using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteMgr : MonoBehaviour
{
    public GameObject RoulettePlate;//������ ȸ���ϰԵ� �귿 ��ü
    public GameObject RoulettePanel;//�귿 ȭ�� On/Off�� ���� �귿 ���
    public Transform Needle;//�귿�� ��������, ��ų �����ܰ� �Ÿ��� Ȯ���ϱ����� �ٴ� 

    public Sprite[] SkillSprite;// ��ų���������λ��� ��������Ʈ
    public Image[] DisplayItemSlot;// �Ź� �ٲ� ��ų �����ܵ��� �� ���� �̹���

    List<int> StartList = new List<int>();//��ġ�� ���� �����̱⸦ ���� ����Ʈ
    List<int> ResultIndexList = new List<int>();//��÷�� ��ų�� �ε����� ������ ����Ʈ
    int ItemCnt = 6;//�귿 ���� �ö� �� ��ų ����

    public GameObject SLotMachine;
    private void Start()
    {
        //Set Random Skill Icon  1. ����Ʈ�� ������ ���
        for (int i = 0; i < ItemCnt; i++)
        {
            StartList.Add(i);
        }
        //Set Random Skill Icon  2. ����Ʈ�� ��ϵ� �����Ϳ� �������� �����Ͽ� ���Կ� �ϳ��� ����
        for (int i = 0; i < ItemCnt; i++)
        {
            int randomIndex = Random.Range(0, StartList.Count);
            ResultIndexList.Add(StartList[randomIndex]);
            DisplayItemSlot[i].sprite = SkillSprite[StartList[randomIndex]];
            StartList.RemoveAt(randomIndex);//�ߺ� ��ų�� �귿�� �ö������ϵ��� ���� �����͸� ���� ����Ʈ���� ����
        }
        
        StartCoroutine(StartRoulette());
    }
    IEnumerator StartRoulette()
    {
        yield return new WaitForSeconds(2f);
        float randomSpd = Random.Range(1.0f, 5.0f);
        float rotateSpeed = 100f * randomSpd;
        // Rotate Roulette 1. ���ѹݺ������� Lerp�Լ��� ������ ������ �ӵ��� ȸ���ϴ� �귿
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
    // Set Slot Item  1.�ٴð� ��� ������ ��ġ�� ����Ͽ� ���� ����� �ε����� ã�Ƴ���, �ش� �ε����� �������� ������
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

        StartCoroutine(StartSlotMachine());
    }

    IEnumerator StartSlotMachine()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("���Ըӽ�");
        SLotMachine.SetActive(true);
    }
}
