using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMovement : MonoBehaviour
{
    #region singletone
    private static JoyStickMovement single;
    public static JoyStickMovement Single
    {
        get
        {
            if (single == null)
            {
                single = FindObjectOfType<JoyStickMovement>();

                if (single == null)
                {
                    var instanceContainer = new GameObject( "JoyStickMovement" );
                    single = instanceContainer.AddComponent<JoyStickMovement>();
                }
            }
            return single;
        }
    }
    #endregion

    [Header("# Image JoyStick")]
    public GameObject obj_bgStick;
    public GameObject obj_smallStick;

    [Header("# Position JoyStick")]
    private Vector3 vec_stickDefaultPosition;
    private Vector3 vec_stickFirstPosition;
    public Vector3 vec_stickNowPosition;

    float f_stckRadius;//조이스틱 배경인 bgStick의 반지름을 통해 스틱이 배경밖으로 나가지못하게 하기위한 변수

    public bool isPlayerMoving = false;
    private void Start()
    {
        f_stckRadius = obj_bgStick.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        vec_stickDefaultPosition = obj_bgStick.transform.position;
    }

    #region EventTrigger
    public void PointDown()
    {
        Debug.Log("Point Down");
        //처음 터지된 화면의 좌표로 조이스틱 이동
        obj_bgStick.transform.position = Input.mousePosition;
        obj_smallStick.transform.position = Input.mousePosition;

        //사용자가 드래그하는 방향을 확인하기위해 첫 터치지점의 좌표를 저장
        vec_stickFirstPosition = Input.mousePosition;

        //TODO: SetTrigger Walk
        if (!PlayerMovement.Single.anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            Debug.Log ("Joy Walk!" );
            PlayerMovement.Single.anim.SetBool("Idel", false);
            PlayerMovement.Single.anim.SetBool("Atk", false);
            PlayerMovement.Single.anim.SetBool("Walk", true);
        }

        isPlayerMoving = true;
        PlayerTargeting.Single.getATarget = false;
    }

    public void Drag( BaseEventData _baseEventData)
    {
        Debug.Log("Point Drag");
        //포인트 이벤트 데이터를 이용하여 드래그되고있는 위치를 받아와서, Position값으로 변환
        PointerEventData pointerEventData = _baseEventData as PointerEventData;
        Vector3 DragPosition = pointerEventData.position;
        //변환된 값에서 처음 터치했을때의 위치를 빼면 현재 드래그되고있는 방향을 알 수 있음, 이 방향벡터는 다른스크립트에서 캐릭터를 직접 움직일때 사용
        vec_stickNowPosition = (DragPosition - vec_stickFirstPosition).normalized;

        float f_stickDistance = Vector3.Distance(DragPosition, vec_stickFirstPosition);

        if (f_stickDistance < (f_stckRadius * 0.55f))
        {
            obj_smallStick.transform.position = vec_stickFirstPosition + vec_stickNowPosition * f_stickDistance;

        }
        else
        {
            obj_smallStick.transform.position = vec_stickFirstPosition + vec_stickNowPosition * (f_stckRadius*0.55f);
        }
    }

    public void Drop()
    {
        Debug.Log("Point Drop");
        vec_stickNowPosition = Vector3.zero;
        obj_bgStick.transform.position = vec_stickDefaultPosition;
        obj_smallStick.transform.position = vec_stickDefaultPosition;

        //TODO: SetTrigger Idel
        if (!PlayerMovement.Single.anim.GetCurrentAnimatorStateInfo(0).IsName("Idel"))
        {
            Debug.Log("Joy Idel!");
            PlayerMovement.Single.anim.SetBool("Walk", false);
            PlayerMovement.Single.anim.SetBool("Atk", false);
            PlayerMovement.Single.anim.SetBool("Idel", true);
        }

        isPlayerMoving = false;
    }
    #endregion

    public void GameClear()
    {
        Application.Quit();

#if UNITY_EDITOR
        // Unity Editor에서 실행 중인 경우에는 Editor를 종료합니다.
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
