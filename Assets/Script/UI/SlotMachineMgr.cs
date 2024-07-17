using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineMgr : MonoBehaviour
{
    [Header("���� ������ ���� ������Ʈ")]
    public GameObject[] SlotSkillObject;//��ų �������� �׷����� ������ ������ ������Ʈ �迭

    [Header("��ư")] public Button[] Slot;
    [Header("��ų ������")] public Sprite[] SkillSprite;
    
    [System.Serializable]
    public class DisplayItemSlot//�Ź� ����� ��ų �����ܵ��� �� �������� ���� �̹����� 2�����迭ó�� �����ϱ����� �迭�� ����Ʈ
    {
        public List<Image> SlotSprite = new List<Image>();
    }
    [Header("��ų�����ܰ� �������� �� 2���� ���� �迭")] public DisplayItemSlot[] DisplayItemSlots;

    public Image DisplayResultImage;//��ų�� �����ϰ� ������ �̹���

    public List<int> StartList = new List<int>();//���� �̱⸦ ���� ����Ʈ
    public List<int> ResultIndexList = new List<int>();//��÷�� ��ų�� �ε����� ������ ����Ʈ
    int ItemCnt = 3;//���� ���� �ö� �� ��ų�� ����
    int[] answer = { 2, 3, 1 };

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ItemCnt * Slot.Length; i++)
        {
            StartList.Add(i);
        }
        //2�� for������ ��ư�ε������� ��ų������ ��ȸ
        for (int i = 0; i < Slot.Length; i++)
        {
            for (int j = 0; j < ItemCnt; j++)
            {
                Slot[i].interactable = false;

                int randomIndex = Random.Range(0, StartList.Count);

                //��ų�� ������ �����ϰ� ����
                //1��° ��ư�϶��� 1�ε����� ��ų, 2��° ��ư�϶��� 0�ε����� ��ų, 3��° ��ư�϶��� 2�ε����� ��ų�� ����
                if (i == 0 && j == 1 || i == 1 && j == 0 || i == 2 && j == 2)
                {
                    ResultIndexList.Add(StartList[randomIndex]);
                }
                //��÷�� ��ų�� �ε����� ����
                DisplayItemSlots[i].SlotSprite[j].sprite = SkillSprite[StartList[randomIndex]];

                //ó�� �׸��� ������ �׸��� �����ϰ� ����
                if (j == 0)
                {
                    DisplayItemSlots[i].SlotSprite[ItemCnt].sprite = SkillSprite[StartList[randomIndex]];
                }
                StartList.RemoveAt(randomIndex);
            }
        }

        for (int i = 0; i < Slot.Length; i++)
        {
            StartCoroutine(StartSlot(i));
        }
    }

    IEnumerator StartSlot(int SlotIndex)
    {
        //������ ��ġ�� �ش��ϴ� UI������Ʈ���� �����Ͽ� 50f�� �������鼭 �����̰�, �ִ������� ������������ ó����ġ���� �ٽ� ����
        /*
         ���Կ�����Ʈ�� y������ 50�� 2�� �̵��ؾ� �׸��ϳ��� �ٲ�
         */
        for (int i = 0; i < (ItemCnt * (6 + SlotIndex * 4) + answer[SlotIndex]) * 2; i++)
        {
            SlotSkillObject[SlotIndex].transform.localPosition -= new Vector3(0, 50f, 0);
            if (SlotSkillObject[SlotIndex].transform.localPosition.y < 50f)
            {
                SlotSkillObject[SlotIndex].transform.localPosition += new Vector3(0, 300f, 0);
            }
            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 0; i < ItemCnt; i++)
        {
            Slot[i].interactable = true;
        }
    }

    public void ClickBtn(int index)
    {
        DisplayResultImage.sprite = SkillSprite[ResultIndexList[index]];
    }
}