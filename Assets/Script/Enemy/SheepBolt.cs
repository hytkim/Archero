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
        //rb.velocity = Vector3.up * -10f;//이부분도 V3.up 정상동작안해서 transform을 활성화함
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            Destroy(gameObject, 0.1f);
        }//el if부분은 내가수정한코드
        else if (collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
