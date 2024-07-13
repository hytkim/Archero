using UnityEngine;

public class Penguin3CuBolt : MonoBehaviour
{
    Rigidbody rb;
    Vector3 NewDir;
    int bounceCnt = 3;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        NewDir = transform.up;
        rb.velocity = NewDir * -50f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(" ###### Penguin 3Cu #######");

        Debug.Log(" Collision Name : " + collision.transform.name);
        if (collision.transform.CompareTag("Wall"))
        {
            bounceCnt--;
            if (bounceCnt > 0)
            {
                Debug.Log("hit wall");
                NewDir = Vector3.Reflect(NewDir, collision.contacts[0].normal);
                rb.velocity = NewDir * -50f;
            }
            else
            {
                Destroy(gameObject, 0.1f);
            }
        }
    }
}