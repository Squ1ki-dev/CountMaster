using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    NavMeshAgent enemyNav;

    GameObject player;

    private void Start()
    {
        enemyNav = GetComponent<NavMeshAgent>();
        player = Player.Instance.gameObject;
    }

    private void Update()
    {
        if (player != null)
            enemyNav.destination = player.transform.position;
    }
}
