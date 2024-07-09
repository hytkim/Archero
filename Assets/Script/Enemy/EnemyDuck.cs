using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDuck : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent nvAgent;

    private void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        nvAgent.SetDestination(player.transform.position);
    }
}
