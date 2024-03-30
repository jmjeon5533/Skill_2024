using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private Transform orientation;
    [SerializeField] private Transform model;
    [SerializeField] private LayerMask modelAlignLayer;

    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Rigidbody rigid;

    public Transform Orientation => orientation;

    [SerializeField] protected int pathIndex;

    private void Start()
    {

    }

    private void Update()
    {
        var g = GameManager.Instance;
        if (!g.isgame) return;
        Movement();

        Physics.Raycast(model.position, Vector3.down, out var hit, 10f, modelAlignLayer);
        model.LookAt(agent.transform.position);
        model.rotation = Quaternion.Lerp(model.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal) * orientation.rotation, Time.deltaTime * 8);
        
    }
    protected virtual void Movement()
    {
        var g = GameManager.Instance;
        if (Vector3.Distance(rigid.position, agent.transform.position) < 10)
        {
            agent.isStopped = false;
            agent.SetDestination(g.curPath[pathIndex].position);
            if (Vector3.Distance(agent.transform.position, g.curPath[pathIndex].position) <= 20)
            {
                pathIndex++;
                if (pathIndex >= g.curPath.Length) pathIndex = 0;
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }
    private void FixedUpdate()
    {
        if (rigid == null || !GameManager.Instance.isgame) return;
            rigid.AddForce((agent.transform.position - rigid.transform.position).normalized * 22, ForceMode.Acceleration);
            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, 25);
    }
}
