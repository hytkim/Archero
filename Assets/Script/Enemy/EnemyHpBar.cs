using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    [Header("Sliders")]
    public Slider hpSlider;
    public Slider backHpSlider;
    public bool backHpHit = false;


    public Transform enemy;
    public float maxHp = 1000f;
    public float curHp = 1000f;

    private void Update()
    {
        transform.position = enemy.position;
        //hpSlider.value = curHp / maxHp;
        hpSlider.value = Mathf.Lerp(hpSlider.value, curHp/maxHp, Time.deltaTime * 5f);
        if (backHpHit)
        {
            backHpSlider.value = Mathf.Lerp(backHpSlider.value, curHp / maxHp, Time.deltaTime * 10f);
            if (hpSlider.value >= backHpSlider.value - 0.01f)//���� Hp�ٰ� ����� ���������
            {
                backHpHit = false;//���� Hp�ٰ� ����ǰ��ϴ� ������ �ʱ�ȭ�Ͽ� ��Hp�� �ٽ� �������ϰ� �ϰ�
                backHpSlider.value = hpSlider.value;//����Hp�ٸ� ���� Hp�ٿ� ������ ������ ������.
            }
        }
    }

    public void Dmg()
    {
        curHp -= 300f;
        Invoke("BackHpFun", 0.5f);
    }
    void BackHpFun()
    {
        backHpHit = true;
    }
}
