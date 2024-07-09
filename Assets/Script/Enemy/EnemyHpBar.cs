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
            if (hpSlider.value >= backHpSlider.value - 0.01f)//뒷쪽 Hp바가 충분히 가까워지면
            {
                backHpHit = false;//뒷쪽 Hp바가 실행되게하는 변수를 초기화하여 뒷Hp가 다시 정상동작하게 하고
                backHpSlider.value = hpSlider.value;//뒷쪽Hp바를 기존 Hp바와 동일한 값으로 맞춰줌.
            }
        }
    }

    public void Dmg()
    {
        curHp -= 250;
        Invoke("BackHpFun", 0.5f);
    }
    void BackHpFun()
    {
        backHpHit = true;
    }
}
