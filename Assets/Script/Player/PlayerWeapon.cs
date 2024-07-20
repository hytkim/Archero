using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    int bounceCnt = 2;
    int wallBounceCnt = 2;
    float dmg = 0f;
    Rigidbody Rb;
    Vector3 NewDir;
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 20f;
        Rb = GetComponent<Rigidbody>();
        NewDir = Vector3.zero;
        dmg = PlayerData.Instance.dmg;

        Destroy(gameObject, 5f);
    }
    
    Vector3 ResultDir(int index)
    {
        int closetIndex = -1;
        float closetDis = 500f;
        float currentDis = 0f;

        for (int i = 0; i < PlayerTargeting.Instance.MonsterList.Count; i++)
        {
            if (i == index) continue;

            currentDis = Vector3.Distance(PlayerTargeting.Instance.MonsterList[i].transform.position, transform.position);

            if (currentDis < 5f) continue;

            if (closetDis > currentDis)
            {
                closetDis = currentDis;
                closetIndex = i;
            }
        }

        if (closetIndex == -1)
        {
            Debug.Log("Someting is worong!");
            Destroy(gameObject, 0.2f);
            return Vector3.zero;
        }

        return (PlayerTargeting.Instance.MonsterList[closetIndex].transform.position - transform.position).normalized;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Monster"))
        {
            if (PlayerData.Instance.PlayerSkill[0] != 0 && PlayerTargeting.Instance.MonsterList.Count >= 2)
            {
                int myIndex = PlayerTargeting.Instance.MonsterList.IndexOf(other.gameObject.transform.parent.gameObject);

                if (bounceCnt > 0)
                {
                    bounceCnt--;
                    dmg *= 0.7f;
                    NewDir = ResultDir(myIndex) * 20f;
                    Rb.velocity = NewDir;
                    return;
                }
            }
            Rb.velocity = Vector3.zero;
            Destroy(gameObject);
        }
        else if (other.transform.CompareTag("Wall"))
        {
            if (PlayerData.Instance.PlayerSkill[4] == 0)
            {
                Rb.velocity = Vector3.zero;
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            if (PlayerData.Instance.PlayerSkill[4] != 0)
            {
                if (wallBounceCnt > 0)
                {
                    wallBounceCnt--;
                    dmg *= 0.5f;
                    NewDir = Vector3.Reflect(NewDir, collision.contacts[0].normal);
                    Rb.velocity = NewDir * 20f;
                    return;
                }
            }

            Rb.velocity = Vector3.zero;
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Name : "+other.transform.name);
        if (other.transform.CompareTag("Wall") || other.transform.CompareTag("Monster"))
        {
            Debug.Log("Name : " + other.transform.name);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Destroy(gameObject, 0.2f);
        }
    }*/
    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Name : "+collision.transform.name);
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Monster"))
        {
            Debug.Log("Name : " + collision.transform.name);
            GetComponent <Rigidbody>().velocity = Vector3.zero;
            Destroy(gameObject, 0.2f);
        }
    }*/
}
