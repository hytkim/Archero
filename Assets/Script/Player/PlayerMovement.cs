using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region singletone
    private static PlayerMovement single;//클래스 내부에서만 접근할 수 있는 정적 변수
    public static PlayerMovement Single//외부에서 private한 single에 접근하기위한 변수
    {
        get
        {
            if (single == null)
            {
                single = FindObjectOfType<PlayerMovement>();//현재 정적 변수인 single이 없으면 현재 씬에서 PlayerMovement객체를 찾아서 등록

                if (single == null)//현재 씬에도 PlayerMoveMent가 없으면 
                {
                    //게임오브젝트를 생성하고 
                    var instanceContainer = new GameObject("PlayerMovement");

                    //생성된 게임오브젝트에 PlayerMovement컴포넌트를 추가
                    single = instanceContainer.AddComponent<PlayerMovement>();
                }
            }
            return single;//생성된 싱글톤 반환
        }
    }
    #endregion

    [Header("# Player Move")]
    public float moveSpeed = 0f;
    private Rigidbody rb;
    
    public Animator anim;
    private void Awake()
    {
        if(single == null)// single톤이 존재하지않는경우, 현재 생성된 개체를 single톤으로 사용한다.
        {
            single = this;
        }else if ( single != this)// 생성된 싱글톤이 이미 존재할 경우
        {
            //현재 객체를 파괴하여 싱글톤의 유일성을 보장
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
