using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySheep : MonoBehaviour
{
    GameObject Player;
    RoomCondition RoomConditionGo;

    public LayerMask layerMask;//Wall

    public GameObject DangerMarker;
    public GameObject EnemyBolt;

    public Transform BoltGenPosition;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); //플레이어가 스테이지에 들어왔을때 동작을 시작해야하니까
        RoomConditionGo = transform.parent.transform.parent.gameObject.GetComponent<RoomCondition>();
        StartCoroutine(WaitPlayer());
    }

    IEnumerator WaitPlayer()
    {
        yield return null;

        while (!RoomConditionGo.playerInThisRoom)
        {
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.5f);
        transform.LookAt(Player.transform.position);
        DangerMarkerShoot();

        yield return new WaitForSeconds(2f);
        Shoot();
    }

    void DangerMarkerShoot()
    {
        Vector3 NewPosition = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);//경고라인을 현재위치에서 바닥위에 그리고
        Physics.Raycast(NewPosition, transform.forward, out RaycastHit hit, 30f, layerMask);//같은위치에서 레이를 발사해서, 내가설정한 layerMask와 부딪히면 종료

        if (hit.transform.CompareTag("Wall"))//발사된 레이가 벽에 맞으면
        {
            GameObject DangerMarkerClone = Instantiate(DangerMarker, NewPosition, transform.rotation);//발사체를 생성
            //DangerMarkerClone.GetComponent<DangerLine>().EndPosition = hit.point;//생성된 발사체의 도착지점을 넘겨주는코드
        }
    }

    void Shoot()
    {
        Instantiate(EnemyBolt, BoltGenPosition.position, transform.rotation);
    }

}
