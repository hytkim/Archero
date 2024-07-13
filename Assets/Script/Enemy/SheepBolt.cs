using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBolt : MonoBehaviour
{
    [SerializeField]Rigidbody rb;
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * -25f;
        //rb.velocity = Vector3.up * -10f;//�̺κе� V3.up �����۾��ؼ� transform�� Ȱ��ȭ��
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            Destroy(gameObject, 0.1f);
        }//el if�κ��� �����������ڵ�
        else if (collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
