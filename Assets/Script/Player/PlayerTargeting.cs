using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    #region singletone
    private static PlayerTargeting single;
    public static PlayerTargeting Single
    {
        get
        {
            if (single == null)
            {
                single = FindObjectOfType<PlayerTargeting>();

                if (single == null)
                {
                    var instanceContainer = new GameObject("PlayerTargeting");
                    single = instanceContainer.AddComponent<PlayerTargeting>();
                }
            }
            return single;
        }
    }
    #endregion

    public bool getATarget = false;
    float curDist = 0f;         //현재 거리
    float closetDist = 100f;    //가까운 거리
    float targetDist = 100f;    //타겟 거리

    int closeDistIndex = 0;     // 가장 가까운 인덱스
    int targetIndex = -1;       // 타게팅할 인덱스

    int prevTargetIndex = 0;

    public LayerMask layerMask;
    public List<GameObject> MonsterList = new();

    public GameObject playerBolt; // 발사체
    public Transform attackPoint;

    public float aSpd = 1f;

    private void OnDrawGizmos()
    {
        if (getATarget)
        {
            //Debug.Log("Run Gizmo Ray");
            for (int i = 0; i < MonsterList.Count; i++)
            {
                if (MonsterList[i] == null) { return; }

                RaycastHit hit;
                bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position, 
                    out hit, 20f, layerMask);

                if (isHit && hit.transform.CompareTag("Monster"))
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawRay(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position);
            }
        }
    }

    private void Update()
    {
        SetTarget();
        AtkTarget();
    }

    void Attack()
    {
        Debug.Log("Now Attack");
        PlayerMovement.Single.anim.SetFloat("AtkSpd", aSpd);
        Instantiate( playerBolt, attackPoint.position, transform.rotation);
    }
    /*void SetTarget()
    {

        if (MonsterList.Count != 0)
        {
            prevTargetIndex = targetIndex;
            curDist = 0f;
            closetDist = 0f;
            targetIndex = -1;

            for (int i = 0; i < MonsterList.Count; i++)
            {
                if (MonsterList[i] == null) { return; }
                //curDist = Vector3.Distance(transform.position, MonsterList[i].transform.position);
                curDist = Vector3.Distance(transform.position, MonsterList[i].transform.GetChild(0).position);

                RaycastHit hit;
                //bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.position - transform.position, out hit, 20f, layerMask);
                bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position, 
                    out hit, 20f, layerMask );

                if (isHit && hit.transform.CompareTag("Monster"))
                {
                    if (targetDist >= curDist)
                    {
                        targetIndex = i;
                        targetDist = curDist;

                        if (!JoyStickMovement.Single.isPlayerMoving && prevTargetIndex != targetIndex)
                        {
                            targetIndex = prevTargetIndex;
                        }
                    }
                }

                //장애물과 상관없이 가장 가까운오브젝트를 갱신
                if (closetDist >= curDist)
                {
                    closetDist = i;
                    closetDist = curDist;
                }

            }

            if (targetIndex == -1)
            {
                targetIndex = closeDistIndex;
            }
            closetDist = 100f;
            targetDist = 100f;
            getATarget = true;

        }

    }*/

    /*void AtkTarget()
    {
        if (targetIndex == -1 || MonsterList.Count == 0)
        {
            PlayerMovement.Single.anim.SetBool("Atk", false);
            return;
        }

        if (getATarget && !JoyStickMovement.Single.isPlayerMoving && MonsterList.Count != 0)
        {
            //transform.LookAt(new Vector3(MonsterList[targetIndex].transform.position.x, transform.position.y, MonsterList[targetIndex].transform.position.y));
            transform.LookAt(MonsterList[targetIndex].transform.GetChild(0));

            if (PlayerMovement.Single.anim.GetCurrentAnimatorStateInfo(0).IsName("Idel"))
            {
                PlayerMovement.Single.anim.SetBool("Idel", false);
                PlayerMovement.Single.anim.SetBool("Walk", false);
                PlayerMovement.Single.anim.SetBool("Atk", true);
            }
        }
        else if (JoyStickMovement.Single.isPlayerMoving)
        {
            if (!PlayerMovement.Single.anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                PlayerMovement.Single.anim.SetBool("Atk", false);
                PlayerMovement.Single.anim.SetBool("Idel", false);
                PlayerMovement.Single.anim.SetBool("Walk", true);
            }
        }
        else
        {
            PlayerMovement.Single.anim.SetBool("Atk", false);
            PlayerMovement.Single.anim.SetBool("Walk", false);
            PlayerMovement.Single.anim.SetBool("Idel", true);
        }
    }*/
    public void SetTarget()
    {
        if (MonsterList.Count != 0)
        {
            prevTargetIndex = targetIndex;
            curDist = 0f;
            closeDistIndex = 0;
            targetIndex = -1;

            for (int i = 0; i < MonsterList.Count; i++)
            {
                if (MonsterList[i] == null) { return; }   // 추가
                curDist = Vector3.Distance(transform.position, MonsterList[i].transform.GetChild(0).position);//변경 

                RaycastHit hit;
                bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.GetChild(0).position - transform.position,//변경 
                                            out hit, 20f, layerMask);

                if (isHit && hit.transform.CompareTag("Monster"))
                {
                    if (targetDist >= curDist)
                    {
                        targetIndex = i;

                        targetDist = curDist;

                        if (!JoyStickMovement.Single.isPlayerMoving && prevTargetIndex != targetIndex)  // 추// 추가
                        {
                            targetIndex = prevTargetIndex;
                        }
                    }
                }

                if (closetDist >= curDist)
                {
                    closeDistIndex = i;
                    closetDist = curDist;
                }
            }

            if (targetIndex == -1)
            {
                targetIndex = closeDistIndex;
            }
            closetDist = 100f;
            targetDist = 100f;
            getATarget = true;
        }

    }
    void AtkTarget()
    {
        if (targetIndex == -1 || MonsterList.Count == 0)  // 추가 
        {
            PlayerMovement.Single.anim.SetBool("Atk", false);
            return;
        }
        if (getATarget && !JoyStickMovement.Single.isPlayerMoving && MonsterList.Count != 0)
        {
            //            Debug.Log ( "lookat : " + MonsterList[TargetIndex].transform.GetChild ( 0 ) );  // 변경
            transform.LookAt(MonsterList[targetIndex].transform.GetChild(0));     // 변경

            if (PlayerMovement.Single.anim.GetCurrentAnimatorStateInfo(0).IsName("Idel"))
            {
                PlayerMovement.Single.anim.SetBool("Idel", false);
                PlayerMovement.Single.anim.SetBool("Walk", false);
                //PlayerMovement.Single.anim.SetBool ("Atk", true );
            }

        }
        else if (JoyStickMovement.Single.isPlayerMoving)
        {
            if (!PlayerMovement.Single.anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                PlayerMovement.Single.anim.SetBool("Atk", false);
                PlayerMovement.Single.anim.SetBool("Idel", false);
                PlayerMovement.Single.anim.SetBool("Walk", true);
            }
        }
        else
        {
            PlayerMovement.Single.anim.SetBool("Atk", false);
            PlayerMovement.Single.anim.SetBool("Idel", true);
            PlayerMovement.Single.anim.SetBool("Walk", false);
        }
    }
}
