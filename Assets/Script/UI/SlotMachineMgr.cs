using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineMgr : MonoBehaviour
{
    [Header("실제 움직일 슬롯 오브젝트")]
    public GameObject[] SlotSkillObject;//스킬 아이콘이 그려지고 실제로 움직일 오브젝트 배열

    [Header("버튼")] public Button[] Slot;
    [Header("스킬 아이콘")] public Sprite[] SkillSprite;
    
    [System.Serializable]
    public class DisplayItemSlot//매번 변경될 스킬 아이콘들이 들어갈 슬롯으로 사용될 이미지를 2차원배열처럼 관리하기위한 배열과 리스트
    {
        public List<Image> SlotSprite = new List<Image>();
    }
    [Header("스킬아이콘과 아이콘이 들어갈 2차원 슬롯 배열")] public DisplayItemSlot[] DisplayItemSlots;

    public Image DisplayResultImage;//스킬을 선택하고 보여줄 이미지

    public List<int> StartList = new List<int>();//랜덤 뽑기를 위한 리스트
    public List<int> ResultIndexList = new List<int>();//당첨된 스킬의 인덱스를 저장할 리스트
    int ItemCnt = 3;//슬롯 위에 올라갈 총 스킬의 갯수
    int[] answer = { 2, 3, 1 };

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ItemCnt * Slot.Length; i++)
        {
            StartList.Add(i);
        }
        //2중 for문으로 버튼인덱스마다 스킬슬롯을 순회
        for (int i = 0; i < Slot.Length; i++)
        {
            for (int j = 0; j < ItemCnt; j++)
            {
                Slot[i].interactable = false;

                int randomIndex = Random.Range(0, StartList.Count);

                //스킬의 순서를 랜덤하게 세팅
                //1번째 버튼일때는 1인덱스의 스킬, 2번째 버튼일때는 0인덱스의 스킬, 3번째 버튼일때는 2인덱스의 스킬을 저장
                if (i == 0 && j == 1 || i == 1 && j == 0 || i == 2 && j == 2)
                {
                    ResultIndexList.Add(StartList[randomIndex]);
                }
                //당첨된 스킬의 인덱스를 저장
                DisplayItemSlots[i].SlotSprite[j].sprite = SkillSprite[StartList[randomIndex]];

                //처음 그림과 마지막 그림을 동일하게 설정
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
        //슬롯의 위치에 해당하는 UI컴포넌트값을 수정하여 50f씩 내려가면서 움직이고, 최대한으로 내려갔을때는 처음위치에서 다시 시작
        /*
         슬롯오브젝트는 y축으로 50씩 2번 이동해야 그림하나가 바뀜
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