using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomCondition : MonoBehaviour
{
    #region singletone
    private static RoomCondition single;
    public static RoomCondition Single
    {
        get
        {
            if (single == null)
            {
                single = FindObjectOfType<RoomCondition>();

                if (single == null)
                {
                    var instanceContainer = new GameObject("RoomCondition");
                    single = instanceContainer.AddComponent<RoomCondition>();
                }
            }
            return single;
        }
    }
    #endregion
    public List<GameObject> MonsterListInRoom = new();
    public bool playerInThisRoom = false;
    public bool isClearRoom = false;

    private void Update()
    {
        if (playerInThisRoom)
        {
            if (MonsterListInRoom.Count <= 0 && !isClearRoom)
            {
                isClearRoom = true;
                Debug.Log("Clear");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInThisRoom = true;
            PlayerTargeting.Instance.MonsterList = new List<GameObject>( MonsterListInRoom );
            Debug.Log("Enter New Room! Mob Cound :"+ PlayerTargeting.Instance.MonsterList.Count);
        }
        if (other.CompareTag("Monster"))
        {
            MonsterListInRoom.Add(other.transform.gameObject);
            Debug.Log("Mob name : "+ other.transform.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInThisRoom= false;
            PlayerTargeting.Instance.MonsterList.Clear();
            Debug.Log("Player Exit!");
        }
        if (other.CompareTag("Monster"))
        {
            MonsterListInRoom.Remove(other.transform.gameObject);
        }

    }
}
