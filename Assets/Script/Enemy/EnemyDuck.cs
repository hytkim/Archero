using System.Collections;
using UnityEngine;

public class EnemyDuck : EnemyMeleeFSM
{
    public GameObject enemyCanvasGo;
    public GameObject meleeAtkArea;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerRealizeRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Start()
    {
        base.Start();
        attackCoolTime = 2f;
        attackCoolTimeCacl = attackCoolTime;

        attackRange = 3f;
        nvAgent.stoppingDistance = 1f;

        StartCoroutine(ResetAtkArea());
    }

    IEnumerator ResetAtkArea()
    {
        while (true)
        {
            yield return null;
            if (!meleeAtkArea.activeInHierarchy && currentState == State.Attack)
            {
                yield return new WaitForSeconds(attackCoolTime);
                meleeAtkArea.SetActive(true);
            }
        }
    }

    protected override void InitMonster()
    {
        maxHp += (StageMgr.Single.currentStage + 1) * 100f;
        currentHp = maxHp;
        damage += (StageMgr.Single.currentStage + 1) * 10f;
    }

    protected override void AtkEffect()
    {
        Instantiate(EffectSet.Single.DuckAtkEffect, transform.position, Quaternion.Euler(90, 0, 0));
    }

    void Update()
    {
        if (currentHp <= 0)
        //if ( enemyCanvasGo.GetComponent<EnemyHpBar> ( ).currentHp <= 0 )
        {
            Debug.Log("Im Die : "+gameObject.name);
            nvAgent.isStopped = true;

            rb.gameObject.SetActive(false);
            

            PlayerTargeting.Instance.MonsterList.Remove(transform.parent.gameObject);
            PlayerTargeting.Instance.TargetIndex = -1;

            /*if (RoomCondition.Single.MonsterListInRoom.Count > 0)
            {
                RoomCondition.Single.MonsterListInRoom.Remove(RoomCondition.Single.MonsterListInRoom[PlayerTargeting.Instance.TargetIndex]);
            }
            else
            {
                Debug.Log("Clear");
            }*/
            

            Destroy(transform.parent.gameObject);
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Potato"))
        {
            enemyCanvasGo.GetComponent<EnemyHpBar>().Dmg();
            currentHp -= 250f;
            Instantiate(EffectSet.Single.DuckDmgEffect, collision.contacts[0].point, Quaternion.Euler(90, 0, 0));
        }
    }
}
