using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class meshEnemy : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent ourAgent;
    public float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ourAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ourAgent.destination = target.transform.position;
    }
}
