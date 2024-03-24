using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemy : Enemy
{
    [SerializeField] private Transform[] path;
    protected override void Movement()
    {
        agent.SetDestination(path[pathIndex].position);
        if (Vector3.Distance(agent.transform.position, path[pathIndex].position) <= 5)
        {
            pathIndex++;
            if (pathIndex >= path.Length) pathIndex = 0;
        }
    }
}
