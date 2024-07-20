using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour//플레이어가 보유한 스킬 인덱스를 저장할 스크립트
{
    #region singletone
    private static PlayerData instance;//클래스 내부에서만 접근할 수 있는 정적 변수
    public static PlayerData Instance//외부에서 private한 single에 접근하기위한 변수
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerData>();//현재 정적 변수인 single이 없으면 현재 씬에서 PlayerMovement객체를 찾아서 등록

                if (instance == null)//현재 씬에도 PlayerMoveMent가 없으면 
                {
                    //게임오브젝트를 생성하고 
                    var instanceContainer = new GameObject("PlayerData");

                    //생성된 게임오브젝트에 PlayerMovement컴포넌트를 추가
                    instance = instanceContainer.AddComponent<PlayerData>();
                }
            }
            return instance;//생성된 싱글톤 반환
        }
    }
    #endregion

    public float dmg = 500;
    //public float maxHp;
    //public float currentHp;
    //public GameObject Player;

    public GameObject[] PlayerBolt;   // 발사체가 많아질경우 배열로 관리

    public List<int> PlayerSkill = new List<int>();// 스킬을 리스트로 관리

}
