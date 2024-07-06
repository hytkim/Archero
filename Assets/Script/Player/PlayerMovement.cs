using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region singletone
    private static PlayerMovement single;//Ŭ���� ���ο����� ������ �� �ִ� ���� ����
    public static PlayerMovement Single//�ܺο��� private�� single�� �����ϱ����� ����
    {
        get
        {
            if (single == null)
            {
                single = FindObjectOfType<PlayerMovement>();//���� ���� ������ single�� ������ ���� ������ PlayerMovement��ü�� ã�Ƽ� ���

                if (single == null)//���� ������ PlayerMoveMent�� ������ 
                {
                    //���ӿ�����Ʈ�� �����ϰ� 
                    var instanceContainer = new GameObject("PlayerMovement");

                    //������ ���ӿ�����Ʈ�� PlayerMovement������Ʈ�� �߰�
                    single = instanceContainer.AddComponent<PlayerMovement>();
                }
            }
            return single;//������ �̱��� ��ȯ
        }
    }
    #endregion

    [Header("# Player Move")]
    public float moveSpeed = 0f;
    private Rigidbody rb;
    
    public Animator anim;
    private void Awake()
    {
        if(single == null)// single���� ���������ʴ°��, ���� ������ ��ü�� single������ ����Ѵ�.
        {
            single = this;
        }else if ( single != this)// ������ �̱����� �̹� ������ ���
        {
            //���� ��ü�� �ı��Ͽ� �̱����� ���ϼ��� ����
            Destroy(gameObject);
        }
    }

    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        if (moveSpeed == 0)
        {
            moveSpeed = 15f;
        }
    }

    private void FixedUpdate()
    {
        if (JoyStickMovement.Single.vec_stickNowPosition.x != 0 || JoyStickMovement.Single.vec_stickNowPosition.y != 0)
        {
            rb.velocity = new Vector3(JoyStickMovement.Single.vec_stickNowPosition.x * moveSpeed, rb.velocity.y, JoyStickMovement.Single.vec_stickNowPosition.y * moveSpeed);

            rb.rotation = Quaternion.LookRotation(new Vector3(JoyStickMovement.Single.vec_stickNowPosition.x, 0, JoyStickMovement.Single.vec_stickNowPosition.y) );
        }
        else
        {
            rb.velocity = Vector3.zero;

            if (!JoyStickMovement.Single.isPlayerMoving)
            {
                anim.SetBool("Atk", true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("NextRoom"))
        {
            Debug.Log("Run Next Room()");
            StageMgr.Single.NextStage();
        }
    }
}
