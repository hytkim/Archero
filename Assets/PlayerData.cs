using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour//�÷��̾ ������ ��ų �ε����� ������ ��ũ��Ʈ
{
    #region singletone
    private static PlayerData instance;//Ŭ���� ���ο����� ������ �� �ִ� ���� ����
    public static PlayerData Instance//�ܺο��� private�� single�� �����ϱ����� ����
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerData>();//���� ���� ������ single�� ������ ���� ������ PlayerMovement��ü�� ã�Ƽ� ���

                if (instance == null)//���� ������ PlayerMoveMent�� ������ 
                {
                    //���ӿ�����Ʈ�� �����ϰ� 
                    var instanceContainer = new GameObject("PlayerData");

                    //������ ���ӿ�����Ʈ�� PlayerMovement������Ʈ�� �߰�
                    instance = instanceContainer.AddComponent<PlayerData>();
                }
            }
            return instance;//������ �̱��� ��ȯ
        }
    }
    #endregion

    public float dmg = 500;
    //public float maxHp;
    //public float currentHp;
    //public GameObject Player;

    public GameObject[] PlayerBolt;   // �߻�ü�� ��������� �迭�� ����

    public List<int> PlayerSkill = new List<int>();// ��ų�� ����Ʈ�� ����

}
