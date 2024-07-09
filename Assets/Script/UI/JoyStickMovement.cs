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
                    var instanceContainer = new GameObject("JoyStickMovement");
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

    float f_stckRadius;//���̽�ƽ ����� bgStick�� �������� ���� ��ƽ�� �������� ���������ϰ� �ϱ����� ����

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
        //ó�� ������ ȭ���� ��ǥ�� ���̽�ƽ �̵�
        obj_bgStick.transform.position = Input.mousePosition;
        obj_smallStick.transform.position = Input.mousePosition;

        //����ڰ� �巡���ϴ� ������ Ȯ���ϱ����� ù ��ġ������ ��ǥ�� ����
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
        PlayerTargeting.Instance.getATarget = false;
    }

    public void Drag( BaseEventData _baseEventData)
    {
        Debug.Log("Point Drag");
        //����Ʈ �̺�Ʈ �����͸� �̿��Ͽ� �巡�׵ǰ��ִ� ��ġ�� �޾ƿͼ�, Position������ ��ȯ
        PointerEventData pointerEventData = _baseEventData as PointerEventData;
        Vector3 DragPosition = pointerEventData.position;
        //��ȯ�� ������ ó�� ��ġ�������� ��ġ�� ���� ���� �巡�׵ǰ��ִ� ������ �� �� ����, �� ���⺤�ʹ� �ٸ���ũ��Ʈ���� ĳ���͸� ���� �����϶� ���
        vec_stickNowPosition = (DragPosition - vec_stickFirstPosition).normalized;

        float f_stickDistance = Vector3.Distance(DragPosition, vec_stickFirstPosition);

        if (f_stickDistance < (f_stckRadius * 0.2f))
        {
            obj_smallStick.transform.position = vec_stickFirstPosition + vec_stickNowPosition * f_stickDistance;
        }
        else
        {
            obj_smallStick.transform.position = vec_stickFirstPosition + vec_stickNowPosition * (f_stckRadius*0.2f);
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
        // Unity Editor���� ���� ���� ��쿡�� Editor�� �����մϴ�.
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
