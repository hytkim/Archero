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
        float scaleX = (1000f/ unitHp) / (maxHp / unitHp);// ���� ������ ü�´� Hp�����̹����� ���� ����
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);// ���̾ƿ� �׷��� ��Ȱ��ȭ/Ȱ��ȭ�� �־�� ������ �����ϰ��� �ݿ���
        foreach (Transform  child in HpLineFolder.transform)
        {
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        }
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
    }
}
