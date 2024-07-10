using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public static PlayerTargeting Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerTargeting>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerTargeting");
                    instance = instanceContainer.AddComponent<PlayerTargeting>();
                }
            }
            return instance;
        }
    }
    private static PlayerTargeting instance;

    public bool getATarget = false;
    float currentDist = 0;      //���� �Ÿ�
    float closetDist = 100f;    //����� �Ÿ�
    float TargetDist = 100f;   //Ÿ�� �Ÿ�
    int closeDistIndex = 0;    //���� ����� �ε���
    public int TargetIndex = -1;      //Ÿ���� �� �ε���
    int prevTargetIndex = 0;
    public LayerMask layerMask;

    public float atkSpd = 1f;

    public List<GameObject> MonsterList = new List<GameObject>();
    //Monster�� ��� List 

    public GameObject PlayerBolt;  //�߻�ü
    public Transform AttackPoint;

    void OnDrawGizmos()
    {
        if (getATarget)
        {
            for (int i = 0; i < MonsterList.Count; i++)
            {
                if (MonsterList[i] == null) { return; }// �߰�
                RaycastHit hit; //	
                bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position,//���� 
                                            out hit, 20f, layerMask);

                if (isHit && hit.transform.CompareTag("Monster"))
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawRay(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position);//���� 
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        SetTarget();
        AtkTarget();
    }

    void Attack()
    {
        PlayerMovement.Single.anim.SetFloat("AttackSpeed", atkSpd);
        Instantiate(PlayerBolt, AttackPoint.position, transform.rotation);
    }

    void SetTarget()
    {
        if (MonsterList.Count != 0)
        {
            prevTargetIndex = TargetIndex;
            currentDist = 0f;
            closeDistIndex = 0;
            TargetIndex = -1;

            for (int i = 0; i < MonsterList.Count; i++)
            {
                if (MonsterList[i] == null) { return; }   // �߰�
                currentDist = Vector3.Distance(transform.position, MonsterList[i].transform.GetChild(0).position);//���� 

                RaycastHit hit;
                bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position,//���� 
                                            out hit, 20f, layerMask);

                if (isHit && hit.transform.CompareTag("Monster"))
                {
                    if (TargetDist >= currentDist)
                    {
                        TargetIndex = i;

                        TargetDist = currentDist;

                        if (!JoyStickMovement.Single.isPlayerMoving && prevTargetIndex != TargetIndex)  // ��// �߰�
                        {
                            TargetIndex = prevTargetIndex;
                        }
                    }
                }

                if (closetDist >= currentDist)
                {
                    closeDistIndex = i;
                    closetDist = currentDist;
                }
            }

            if (TargetIndex == -1)
            {
                TargetIndex = closeDistIndex;
            }
            closetDist = 100f;
            TargetDist = 100f;
            getATarget = true;
        }

    }

    void AtkTarget()
    {
        if (TargetIndex == -1 || MonsterList.Count == 0)  // �߰� 
        {
            PlayerMovement.Single.anim.SetBool("Attack", false);
            //Debug.Log("Atarget False");
            return;
        }
        if (getATarget && !JoyStickMovement.Single.isPlayerMoving && MonsterList.Count != 0)
        {
            //            Debug.Log ( "lookat : " + MonsterList[TargetIndex].transform.GetChild ( 0 ) );  // ����
            transform.LookAt(MonsterList[TargetIndex].transform.GetChild(0));// ����

            if (PlayerMovement.Single.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                PlayerMovement.Single.anim.SetBool("Idle", false);
                PlayerMovement.Single.anim.SetBool("Walk", false);
                PlayerMovement.Single.anim.SetBool("Attack", true);
            }

        }
        else if (JoyStickMovement.Single.isPlayerMoving)
        {
            if (!PlayerMovement.Single.anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                PlayerMovement.Single.anim.SetBool("Attack", false);
                PlayerMovement.Single.anim.SetBool("Idle", false);
                PlayerMovement.Single.anim.SetBool("Walk", true);
            }
        }
        else
        {
            PlayerMovement.Single.anim.SetBool("Attack", false);
            PlayerMovement.Single.anim.SetBool("Idle", true);
            PlayerMovement.Single.anim.SetBool("Walk", false);
        }
    }
}
