using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public Transform player;
    public Slider hpBar;
    public float maxHp;
    public float curHp;

    public GameObject HpLineFolder;
    float unitHp = 200f;

    private void Update()
    {
        transform.position = player.position;
        hpBar.value = curHp / maxHp;
    }

    public void GetHpBoost()
    {
        maxHp += 150;
        float scaleX = (1000f/ unitHp) / (maxHp / unitHp);// 내가 설정한 체력당 Hp유닛이미지가 들어가는 개수
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);// 레이아웃 그룹의 비활성화/활성화가 있어야 수정한 스케일값이 반영됨
        foreach (Transform  child in HpLineFolder.transform)
        {
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        }
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
    }
}
