using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject Player;
    public float distance;
    public NavMeshAgent _agent;
    public EnemyStateManager stateCheck;
    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(Player.transform.position, this.transform.position);
        //the this refers to the enemy base class
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
