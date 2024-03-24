using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private Transform orientation;
    [SerializeField] private Transform model;
    [SerializeField] private LayerMask modelAlignLayer;

    protected NavMeshAgent agent;

    public Transform Orientation => orientation;

    [SerializeField] protected int pathIndex;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        var g = GameManager.Instance;
        if (!g.isgame) return;
        Movement();

        Physics.Raycast(model.position, Vector3.down, out var hit, 10f, modelAlignLayer);
        model.rotation = Quaternion.Lerp(model.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal) * orientation.rotation, Time.deltaTime * 8);
    }
    protected virtual void Movement()
    {
        var g = GameManager.Instance;
        agent.SetDestination(g.curPath[pathIndex].position);
        if (Vector3.Distance(agent.transform.position, g.curPath[pathIndex].position) <= 5)
        {
            pathIndex++;
            if (pathIndex >= g.curPath.Length) pathIndex = 0;
        }
    }
}
