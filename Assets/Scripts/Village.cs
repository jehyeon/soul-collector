using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Village : MonoBehaviour
{
    void Start()
    {
        // 마을에서 던전 가는 포탈 재 생성
        PortalSystem.Instance.CreatePortal(new Vector3(10f, 2f, 0), PortalType.GoDungeon);
        
        // Player reset
        Player player = GameObject.Find("Player").GetComponent<Player>();
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        player.IdleMode();
        agent.enabled = false;
        player.transform.position = Vector3.zero;
        agent.enabled = true;
    }
}
